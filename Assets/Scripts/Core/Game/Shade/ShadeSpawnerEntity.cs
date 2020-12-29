﻿using System;
using AChildsCourage.Game.Floors;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Shade
{

    public class ShadeSpawnerEntity : MonoBehaviour
    {

        [Pub] public event EventHandler OnShadeSpawned;


        [SerializeField] private float shadeTimeoutTime;
        [FindInScene] private ShadeBrainEntity shadeBrain;

        private Vector2 spawnPosition;


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs)
        {
            FindSpawnPosition(eventArgs.Floor);
            SpawnShade();
        }

        private void FindSpawnPosition(Floor floor) =>
            spawnPosition = floor.Map(FindEndChunkCenter).Map(ToVector2);


        [Sub(nameof(ShadeBodyEntity.OnShadeOutOfBounds))]
        private void OnShadeBanished(object _1, EventArgs _2) =>
            TimeoutShade();

        private void TimeoutShade() =>
            Invoke(nameof(SpawnShade), shadeTimeoutTime);


        private void SpawnShade()
        {
            TeleportShadeToSpawn();
            OnShadeSpawned?.Invoke(this, EventArgs.Empty);
        }

        private void TeleportShadeToSpawn() => shadeBrain.transform.position = spawnPosition;

    }

}