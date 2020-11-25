using Castle.Core.Internal;
using Ninject.Extensions.AppccelerateEventBroker;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ninject.Extensions.Unity
{

    public static class UnityEntityBinding
    {

        public static void BindUnityEntities(this IKernel kernel)
        {
            var entities =
                GetAllMonoBehaviours()
                .Where(IsEntity);

            foreach (var entity in entities)
                BindEntity(kernel, entity);
        }

        private static IEnumerable<MonoBehaviour> GetAllMonoBehaviours()
        {
            return UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
        }

        private static bool IsEntity(MonoBehaviour monoBehaviour)
        {
            var type = monoBehaviour.GetType();

            return type.HasAttribute<UnityEntityAttribute>();
        }

        private static void BindEntity(IKernel kernel, MonoBehaviour entity)
        {
            var interfaceType = GetInterfaceType(entity);

            kernel.Bind(interfaceType).ToConstant(entity);
        }

        private static Type GetInterfaceType(MonoBehaviour entity)
        {
            var entityAttribute = entity.GetType().GetAttribute<UnityEntityAttribute>();

            return entityAttribute.InterfaceType;
        }

    }

}