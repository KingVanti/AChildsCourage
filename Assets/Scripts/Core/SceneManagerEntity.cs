using System;
using UnityEngine;

namespace AChildsCourage
{

    public class SceneManagerEntity : MonoBehaviour
    {

        [Pub] public event EventHandler OnSceneLoaded;


        private void Awake() =>
            SetupScene();

        private void SetupScene()
        {
            Infrastructure.SetupSceneInfrastructure();
            OnSceneLoaded?.Invoke(this, EventArgs.Empty);
        }

    }

}