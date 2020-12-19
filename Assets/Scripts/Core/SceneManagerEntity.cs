using System;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.Infrastructure.MInfrastructure;

namespace AChildsCourage
{

    public class SceneManagerEntity : MonoBehaviour
    {

        [Pub] public event EventHandler OnSceneLoaded;

        private void Awake() => SetupScene();

        private void SetupScene()
        {
            SetupSceneInfrastructure();
            OnSceneLoaded?.Invoke(this, EventArgs.Empty);
        }

    }

}