using System;
using UnityEngine;
using static AChildsCourage.Infrastructure;

namespace AChildsCourage
{

    public class SceneManagerEntity : MonoBehaviour
    {

        [Pub] public event EventHandler OnSceneLoaded;


        private void Awake() =>
            SetupScene();

        private void SetupScene()
        {
            SetupSceneInfrastructure();
            OnSceneLoaded?.Invoke(this, EventArgs.Empty);
            OnSceneSetupComplete();
        }

        protected virtual void OnSceneSetupComplete() { }

    }

}