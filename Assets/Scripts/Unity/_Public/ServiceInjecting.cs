using AChildsCourage.Game;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.Persistance;
using Ninject;
using Ninject.Extensions.AppccelerateEventBroker;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            var assemblies = GetAssemblies().ToArray();

            BindSingletons(kernel, assemblies);
            BindNonSingletons(kernel, assemblies);
            BindConstants(kernel);
            kernel.BindUnityEntities();
            
            ActivateEagerServices(kernel);

            kernel.AutoInjectSceneServices(assemblies.First(a => a.FullName.Contains(UnityAssemblyName)));
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

        private static void BindSingletons(IKernel kernel, IEnumerable<Assembly> assemblies)
        {
            kernel.Bind(x => x
                .From(assemblies)
               .IncludingNonPublicTypes().SelectAllClasses().WithAttribute<SingletonAttribute>()
               .BindAllInterfaces()
               .Configure(b => b.InSingletonScope().RegisterOnEventBroker(DefaultEventBrokerName)));
        }

        private static void BindNonSingletons(IKernel kernel, IEnumerable<Assembly> assemblies)
        {
            kernel.Bind(x => x
                .From(assemblies)
                .IncludingNonPublicTypes().SelectAllClasses().WithoutAttribute<SingletonAttribute>()
                .BindAllInterfaces()
                .Configure(b => b.RegisterOnEventBroker(DefaultEventBrokerName)));
        }

        private static void BindConstants(IKernel kernel)
        {
            kernel.Bind<RoomDataLoader>().ToConstant(RoomDataLoading.Make());
            kernel.Bind<RunDataLoader>().ToConstant(JsonRunDataLoading.Make());
        }

        private static void ActivateEagerServices(IKernel kernel)
        {
            _ = kernel.GetAll<IEagerActivation>().ToArray();
        }

    }

#pragma warning restore 1701, 1702

}