using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AChildsCourage.Game;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Items;
using AChildsCourage.Game.Persistence;
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
        private const string UnityAssemblyName = "Unity";


        internal static void InjectServices()
        {
            var kernel = CreateDefaultKernel();
            var assemblies = GetAssemblies()
                .ToArray();
            var unityAssembly = assemblies.First(a => a.FullName.Contains(UnityAssemblyName));
            var monoBehaviourTypes = unityAssembly.GetTypes()
                                                  .Where(t => typeof(MonoBehaviour).IsAssignableFrom(t))
                                                  .ToArray();

            BindSingletons(kernel, assemblies, monoBehaviourTypes);
            BindNonSingletons(kernel, assemblies, monoBehaviourTypes);
            BindConstants(kernel);
            kernel.BindUnityEntities();
            kernel.RegisterMonoBehaviours(DefaultEventBrokerName);

            ActivateEagerServices(kernel);

            kernel.AutoInjectSceneServices(unityAssembly);
        }

        private static IKernel CreateDefaultKernel()
        {
            var kernel = new StandardKernel();

            kernel.AddGlobalEventBroker(DefaultEventBrokerName);

            return kernel;
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return Assembly.Load("AChildsCourage.Core");
            yield return Assembly.Load("AChildsCourage.Unity");
        }

        private static void BindSingletons(IBindingRoot root, IEnumerable<Assembly> assemblies, IEnumerable<Type> monoBehaviourTypes)
        {
            root.Bind(x => x
                           .From(assemblies)
                           .IncludingNonPublicTypes()
                           .SelectAllClasses()
                           .WithAttribute<SingletonAttribute>()
                           .Excluding(monoBehaviourTypes)
                           .BindAllInterfaces()
                           .Configure(b => b.InSingletonScope()
                                            .RegisterOnEventBroker(DefaultEventBrokerName)));
        }

        private static void BindNonSingletons(IKernel kernel, IEnumerable<Assembly> assemblies, IEnumerable<Type> monoBehaviourTypes)
        {
            kernel.Bind(x => x
                             .From(assemblies)
                             .IncludingNonPublicTypes()
                             .SelectAllClasses()
                             .WithoutAttribute<SingletonAttribute>()
                             .Excluding(monoBehaviourTypes)
                             .BindAllInterfaces()
                             .Configure(b => b.RegisterOnEventBroker(DefaultEventBrokerName)));
        }

        private static void BindConstants(IBindingRoot root)
        {
            root.Bind<LoadRoomData>()
                .ToConstant(RoomDataLoading.Make());
            root.Bind<LoadRunData>()
                .ToConstant(JsonRunDataLoading.Make());
            root.Bind<FindItemData>()
                .ToConstant(ItemDataRepository.GetItemDataFinder());
            root.Bind<LoadItemIds>()
                .ToConstant(ItemDataRepository.GetItemIdLoader());
        }

        private static void ActivateEagerServices(IResolutionRoot root)
        {
            _ = root.GetAll<IEagerActivation>()
                    .ToArray();
        }

    }

#pragma warning restore 1701, 1702

}