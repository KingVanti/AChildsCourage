using System;
using System.Collections;
using AChildsCourage.Game.Floors;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Shade
{

    public class ShadeSpawnerEntity : MonoBehaviour
    {

        private TilePosition spawnTile;

        [Pub] public event EventHandler OnShadeSpawned;


        public void SpawnShade()
        {
            TeleportShadeToSpawn();
            OnShadeSpawned?.Invoke(this, EventArgs.Empty);
        }

        private void TeleportShadeToSpawn() => shadeBrain.transform.position = spawnTile.Map(ToVector2);


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs)
        {
            spawnTile = GetCenter(eventArgs.Floor.EndRoomChunkPosition);
            SpawnShade();
        }


        [Sub(nameof(ShadeBrainEntity.OnShadeBanished))]
        private void OnShadeBanished(object _1, EventArgs _2) => StartCoroutine(TimeoutShade());

        private IEnumerator TimeoutShade()
        {
            yield return new WaitForSeconds(shadeTimeoutTime);
            SpawnShade();
        }

#pragma warning disable 649

        [SerializeField] private float shadeTimeoutTime;

        [FindInScene] private ShadeBrainEntity shadeBrain;

#pragma warning restore 649

    }

}