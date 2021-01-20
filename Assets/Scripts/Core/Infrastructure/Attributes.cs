using System;

namespace AChildsCourage
{

    [AttributeUsage(AttributeTargets.Field)]
    public class FindInSceneAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    internal class FindServiceAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    internal class ServiceAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Event)]
    internal class PubAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    internal class SubAttribute : Attribute
    {

        internal string EventName { get; }


        internal SubAttribute(string eventName) => EventName = eventName;

    }

    [AttributeUsage(AttributeTargets.Field)]
    public class FindComponentAttribute : Attribute
    {

        public ComponentFindMode FindMode { get; }


        internal FindComponentAttribute(ComponentFindMode findMode = ComponentFindMode.OnSelf) =>
            FindMode = findMode;

    }

}