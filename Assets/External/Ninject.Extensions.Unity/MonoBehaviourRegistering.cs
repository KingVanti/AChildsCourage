﻿using Ninject.Extensions.AppccelerateEventBroker;
using Ninject.Infrastructure.Language;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ninject.Extensions.Unity
{

    public static class MonoBehaviourRegistering
    {

        public static void RegisterMonoBehaviours(this IKernel kernel, string eventBrokerName)
        {
            var behaviours =
                GetAllMonoBehaviours()
                .Where(ShouldRegister);

            foreach (var behaviour in behaviours)
                Register(kernel, behaviour, eventBrokerName);
        }

        private static IEnumerable<MonoBehaviour> GetAllMonoBehaviours()
        {
            return Object.FindObjectsOfType<MonoBehaviour>();
        }

        private static bool ShouldRegister(MonoBehaviour monoBehaviour)
        {
            var type = monoBehaviour.GetType();

            return type.HasAttribute<UseDIAttribute>();
        }

        private static void Register(IKernel kernel, MonoBehaviour behaviour, string eventBrokerName)
        {
            kernel.Bind(behaviour.GetType()).ToConstant(behaviour).RegisterOnEventBroker(eventBrokerName);
            _ = kernel.Get(behaviour.GetType());
        }

    }

}