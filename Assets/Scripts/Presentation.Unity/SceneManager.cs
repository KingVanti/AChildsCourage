using Ninject;
using Ninject.Extensions.AppccelerateEventBroker;
using Ninject.Extensions.Conventions;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage
{

    public class SceneManager : MonoBehaviour
    {

        #region Fields

        public UnityEvent onSceneLoaded;

        #endregion

        #region Methods

        private void Awake()
        {
            SetupScene();
        }

        private void SetupScene()
        {
            AutoInjectServices();
            onSceneLoaded.Invoke();
        }

        private void AutoInjectServices()
        {
#pragma warning disable 1701, 1702

            var kernel = new StandardKernel();

            kernel.AddGlobalEventBroker("Default");

            var assemblies = new[]
            {
                Assembly.Load("AChildsCourage.Core"),
                Assembly.Load("AChildsCourage.Presentation.Core"),
                Assembly.Load("AChildsCourage.Presentation.Unity")
            };

            kernel.Bind(x => x
                .From(assemblies)
                .IncludingNonPublicTypes().SelectAllClasses().WithAttribute<SingletonAttribute>()
                .BindAllInterfaces()
                .Configure(b => b.InSingletonScope().RegisterOnEventBroker("Default")));

            kernel.Bind(x => x
               .From(assemblies)
               .IncludingNonPublicTypes().SelectAllClasses().WithoutAttribute<SingletonAttribute>()
               .BindAllInterfaces()
               .Configure(b => b.RegisterOnEventBroker("Default")));

            _ = kernel.GetAll<IEagerActivation>().ToArray();

            kernel.AutoInjectSceneServices(assemblies.Where(a => a.FullName.Contains("Unity")));

#pragma warning restore 1701, 1702
        }

        #endregion

    }

}