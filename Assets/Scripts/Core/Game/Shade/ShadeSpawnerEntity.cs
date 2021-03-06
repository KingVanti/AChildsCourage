﻿using System;
using AChildsCourage.Game.Floors;
using JetBrains.Annotations;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeSpawnerEntity : MonoBehaviour
    {

        [Pub] public event EventHandler OnShadeSpawned;


        [SerializeField] private float shadeTimeoutTime;

        [FindInScene] private ShadeBrainEntity shadeBrain;
        [FindInScene] private ShadeDirectorEntity shadeDirector;


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))] [UsedImplicitly]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs) =>
            this.DoAfter(SpawnShade, 0.1f);

        [Sub(nameof(ShadeBodyEntity.OnShadeOutOfBounds))] [UsedImplicitly]
        private void OnShadeBanished(object _1, EventArgs _2) =>
            TimeoutShade();

        private void TimeoutShade() =>
            this.DoAfter(SpawnShade, shadeTimeoutTime);

        private void SpawnShade()
        {
            TeleportShadeToSpawn();
            OnShadeSpawned?.Invoke(this, EventArgs.Empty);
        }

        private void TeleportShadeToSpawn() =>
            shadeBrain.transform.position = shadeDirector.FindShadeSpawnPoint();

    }

}