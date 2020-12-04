using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Ninject.Infrastructure.Language;
using Ninject.Syntax;
using UnityEngine;
using Object = UnityEngine.Object;

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

        private static IEnumerable<MonoBehaviour> GetAllMonoBehaviours() => Object.FindObjectsOfType<MonoBehaviour>();

        private static bool IsEntity(MonoBehaviour monoBehaviour)
        {
            var type = monoBehaviour.GetType();

            return type.HasAttribute<UnityEntityAttribute>();
        }

        private static void BindEntity(IBindingRoot root, MonoBehaviour entity)
        {
            var interfaceType = GetInterfaceType(entity);

            root.Bind(interfaceType).ToConstant(entity);
        }

        private static Type GetInterfaceType(MonoBehaviour entity)
        {
            var entityAttribute = entity.GetType().GetAttribute<UnityEntityAttribute>();

            return entityAttribute.InterfaceType;
        }

    }

}