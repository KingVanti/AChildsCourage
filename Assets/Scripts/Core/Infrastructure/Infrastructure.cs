using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace AChildsCourage.Infrastructure
{

    [AttributeUsage(AttributeTargets.Field)]
    public class FindInSceneAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class FindServiceAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class ServiceAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Event)]
    public class PubAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class SubAttribute : Attribute
    {

        public string EventName { get; }


        public SubAttribute(string eventName) => EventName = eventName;

    }


    public static class MInfrastructure
    {

        private const BindingFlags DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;


        private static readonly Dictionary<Type, Delegate> services = new Dictionary<Type, Delegate>();
        private static readonly Dictionary<string, (EventInfo Event, object Emitter)> events = new Dictionary<string, (EventInfo Event, object Emitter)>();


        public static void SetupSceneInfrastructure()
        {
            if (services.Count == 0) SetupServices();
            events.Clear();

            SetupMonoBehaviourInfrastructure();
        }

        private static void SetupServices()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                foreach (var property in type.GetProperties().Where(HasAttribute<ServiceAttribute>))
                {
                    if (!typeof(Delegate).IsAssignableFrom(property.PropertyType)) throw new Exception($"Service {property.PropertyType} cannot be registered because it is not a delegate!");

                    services.Add(property.PropertyType, (Delegate) property.GetValue(null));
                }
            }
        }

        private static void SetupMonoBehaviourInfrastructure()
        {
            var monoBehaviours = FindMonoBehavioursForCurrentScene().ToImmutableHashSet();

            monoBehaviours.ForEach(PublishEvents);
            monoBehaviours.ForEach(SetupInfrastructure);
        }

        private static IEnumerable<MonoBehaviour> FindMonoBehavioursForCurrentScene() =>
            UnityObject.FindObjectsOfType<MonoBehaviour>();

        private static void PublishEvents(MonoBehaviour monoBehaviour)
        {
            foreach (var @event in monoBehaviour.GetType().GetEvents(DefaultBindingFlags).Where(HasAttribute<PubAttribute>)) events.Add(@event.Name, (@event, monoBehaviour));
        }


        public static GameObject Spawn(GameObject prefab, Transform parent) =>
            Spawn(prefab, Vector3.zero, Quaternion.identity, parent);

        public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var gameObject = UnityObject.Instantiate(prefab, position, rotation, parent);

            gameObject
                .GetComponents<MonoBehaviour>()
                .ForEach(SetupInfrastructure);

            return gameObject;
        }


        private static void SetupInfrastructure(MonoBehaviour monoBehaviour)
        {
            FindComponentsInSceneFor(monoBehaviour);
            FindServicesFor(monoBehaviour);
            SubscribeToEvents(monoBehaviour);
        }

        private static void FindComponentsInSceneFor(MonoBehaviour monoBehaviour) =>
            GetFieldsWith<FindInSceneAttribute>(monoBehaviour)
                .ForEach(field =>
                {
                    var component = UnityObject.FindObjectOfType(field.FieldType);
                    field.SetValue(monoBehaviour, component);
                });

        private static void FindServicesFor(MonoBehaviour monoBehaviour) =>
            GetFieldsWith<FindServiceAttribute>(monoBehaviour)
                .ForEach(field =>
                {
                    try
                    {
                        var service = services[field.FieldType];
                        field.SetValue(monoBehaviour, service);
                    }
                    catch
                    {
                        throw new Exception($"No service for the type {field.FieldType} was registered!");
                    }
                });

        private static void SubscribeToEvents(MonoBehaviour monoBehaviour) =>
            GetMethodsWith<SubAttribute>(monoBehaviour)
                .ForEach(method =>
                {
                    var eventName = method.GetCustomAttribute<SubAttribute>().EventName;

                    try
                    {
                        var (eventInfo, emitter) = events[eventName];
                        var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, monoBehaviour, method);

                        eventInfo.AddEventHandler(emitter, handler);
                    }
                    catch (KeyNotFoundException)
                    {
                        throw new Exception($"No event with the name \"{eventName}\" published!");
                    }
                    catch (Exception)
                    {
                        throw new Exception($"\"{monoBehaviour.GetType().Name}.{method.Name}\"s signature does not match event {eventName}!");
                    }
                });

        private static IEnumerable<FieldInfo> GetFieldsWith<TAttr>(MonoBehaviour monoBehaviour) where TAttr : Attribute =>
            monoBehaviour
                .GetType()
                .GetFields(DefaultBindingFlags)
                .Where(HasAttribute<TAttr>);

        private static IEnumerable<MethodInfo> GetMethodsWith<TAttr>(MonoBehaviour monoBehaviour) where TAttr : Attribute =>
            monoBehaviour
                .GetType()
                .GetMethods(DefaultBindingFlags)
                .Where(HasAttribute<TAttr>);

        private static bool HasAttribute<TAttr>(MemberInfo member) where TAttr : Attribute =>
            member.GetCustomAttribute<TAttr>() != null;

    }

}