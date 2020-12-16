using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AChildsCourage.Game;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Items;
using Ninject;
using Ninject.Extensions.AppccelerateEventBroker;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Unity;
using Ninject.Syntax;
using UnityEngine;

namespace AChildsCourage
{

#pragma warning disable 1701, 1702

    internal static class ServiceInjecting
    {

        private const string DefaultEventBrokerName = "Default";


        private static Assembly ProjectAssembly => Assembly.Load("AChildsCourage.Core");

        internal static void InjectServices()
        {
            var kernel = CreateDefaultKernel();
            var assembly = ProjectAssembly;
            var monoBehaviourTypes = GetMonoBehaviourTypes(assembly).ToArray();

            BindSingletons(kernel, assembly, monoBehaviourTypes);
            BindNonSingletons(kernel, assembly, monoBehaviourTypes);
            BindConstants(kernel);
            kernel.BindUnityEntities();
            kernel.RegisterMonoBehaviours(DefaultEventBrokerName);

            ActivateEagerServices(kernel);

            kernel.AutoInjectSceneServices(assembly);
        }

        private static IKernel CreateDefaultKernel()
        {
            var kernel = new StandardKernel();

            kernel.AddGlobalEventBroker(DefaultEventBrokerName);

            return kernel;
        }

        private static IEnumerable<Type> GetMonoBehaviourTypes(Assembly unityAssembly) => unityAssembly.GetTypes().Where(t => typeof(MonoBehaviour).IsAssignableFrom(t));

        private static void BindSingletons(IBindingRoot root, Assembly assembly, IEnumerable<Type> monoBehaviourTypes) =>
            root.Bind(x => x.From(assembly)
                            .IncludingNonPublicTypes()
                            .SelectAllClasses()
                            .WithAttribute<SingletonAttribute>()
                            .Excluding(monoBehaviourTypes)
                            .BindAllInterfaces()
                            .Configure(b => b.InSingletonScope().RegisterOnEventBroker(DefaultEventBrokerName)));

        private static void BindNonSingletons(IKernel kernel, Assembly assembly, IEnumerable<Type> monoBehaviourTypes) =>
            kernel.Bind(x => x.From(assembly)
                              .IncludingNonPublicTypes()
                              .SelectAllClasses()
                              .WithoutAttribute<SingletonAttribute>()
                              .Excluding(monoBehaviourTypes)
                              .BindAllInterfaces()
                              .Configure(b => b.RegisterOnEventBroker(DefaultEventBrokerName)));

        private static void BindConstants(IBindingRoot root)
        {
            root.Bind<LoadRoomData>().ToConstant(RoomDataLoading.Make());
            root.Bind<FindItemData>().ToConstant(ItemDataRepository.GetItemDataFinder());
            root.Bind<LoadItemIds>().ToConstant(ItemDataRepository.GetItemIdLoader());
        }

        private static void ActivateEagerServices(IResolutionRoot root) => _ = root.GetAll<IEagerActivation>().ToArray();

    }

#pragma warning restore 1701, 1702

}