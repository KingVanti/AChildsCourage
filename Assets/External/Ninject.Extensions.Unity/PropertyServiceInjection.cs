using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ninject.Syntax;

namespace Ninject.Extensions.Unity
{

    public static class PropertyServiceInjection
    {
        
        public static void AutoInjectSceneServices(this IKernel kernel, Assembly injectionAssembly)
        {
            var types = GetAllTypes(injectionAssembly);

            foreach (var type in types)
                InjectServicesFor(type, kernel);
        }

        private static IEnumerable<Type> GetAllTypes(Assembly injectionAssembly)
        {
            return injectionAssembly.GetTypes();
        }

        private static void InjectServicesFor(Type type, IKernel kernel)
        {
            var monoBehaviours = Utility.GetMonoBehavioursOfType(type).ToArray();

            if (monoBehaviours.Length <= 0)
                return;
            
            foreach (var property in GetAutoInjectProperties(type))
            {
                var service = GetServiceFor(property, kernel);

                foreach (var mono in monoBehaviours)
                    property.SetValue(mono, service);
            }
        }

        private static IEnumerable<PropertyInfo> GetAutoInjectProperties(Type type)
        {
            var properties = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            var autoInjectProperties = properties.Where(IsAutoInjectProperty);

            return autoInjectProperties.ToArray();
        }

        private static bool IsAutoInjectProperty(PropertyInfo property)
        {
            return property.GetCustomAttribute<AutoInjectAttribute>() != null;
        }

        private static object GetServiceFor(PropertyInfo property, IResolutionRoot root)
        {
            var propertyType = property.PropertyType;

            return root.Get(propertyType);
        }

    }

}
