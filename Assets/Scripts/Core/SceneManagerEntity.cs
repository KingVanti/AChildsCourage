﻿using System;
using UnityEngine;
using static AChildsCourage.Infrastructure;

namespace AChildsCourage
{

    public class SceneManagerEntity : MonoBehaviour
    {

        [Pub] public event EventHandler OnSceneBecameVisible;

        [Pub] public event EventHandler OnSceneLoaded;


        private void Awake()
        {
            if (Transition.IsUninitialized)
                Transition.ToSelf();
        }

        internal void OnSceneOpened()
        {
            SetupSceneInfrastructure();
            OnSceneLoaded?.Invoke(this, EventArgs.Empty);
        }

        internal void OnSceneVisible() =>
            OnSceneBecameVisible?.Invoke(this, EventArgs.Empty);

    }

}