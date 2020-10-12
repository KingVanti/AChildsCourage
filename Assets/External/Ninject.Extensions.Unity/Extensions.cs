using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ninject.Extensions.Unity
{

    public static class Extensions
    {

        #region Methods

        public static void AutoInjectSceneServices(this IKernel kernel, IEnumerable<Assembly> injectionAssemblies)
        {
            foreach (var injectionAssembly in injectionAssemblies)
                AutoInjectSceneServices(kernel, injectionAssembly);
        }

        private static void AutoInjectSceneServices(IKernel kernel, Assembly injectionAssembly)
        {
            var types = GetAllTypes(injectionAssembly);

            foreach (var type in types)
                InjectServicesFor(type, kernel);
        }

        private static Type[] GetAllTypes(Assembly injectionAssembly)
        {
            return injectionAssembly.GetTypes();
        }

        private static void InjectServicesFor(Type type, IKernel kernel)
        {
            var monos = Utility.GetMonoBehavioursOfType(type).ToArray();

            if (monos.Length > 0)
            {
                foreach (var property in GetAutoInjectProperties(type))
                {
                    var service = GetServiceFor(property, kernel);

                    foreach (var mono in monos)
                        property.SetValue(mono, service);
                }
            }
        }

        private static PropertyInfo[] GetAutoInjectProperties(Type type)
        {
            var properties = type.GetProperties();
            var autoInjectProperties = properties.Where(IsAutoInjectProperty);

            return autoInjectProperties.ToArray();
        }

        private static bool IsAutoInjectProperty(PropertyInfo property)
        {
            return property.GetCustomAttribute<AutoInjectAttribute>() != null;
        }

        private static object GetServiceFor(PropertyInfo property, IKernel kernel)
        {
            var propertyType = property.PropertyType;

            return kernel.Get(propertyType);
        }

        #endregion

    }

}
