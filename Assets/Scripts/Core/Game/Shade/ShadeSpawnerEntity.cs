using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Shade
{

    public class ShadeSpawnerEntity : MonoBehaviour
    {

        public Events.Empty onShadeSpawned;

        private TilePosition spawnTile;


        public void SpawnShade()
        {
            TeleportShadeToSpawn();
            onShadeSpawned.Invoke();
        }

        private void TeleportShadeToSpawn() => shadeBrain.transform.position = spawnTile.Map(ToVector2);


        public void OnFloorBuilt(Floor floor)
        {
            spawnTile = GetCenter(floor.EndRoomChunkPosition);
            SpawnShade();
        }


        public void OnShadeBanished() => StartCoroutine(TimeoutShade());

        private IEnumerator TimeoutShade()
        {
            yield return new WaitForSeconds(shadeTimeoutTime);
            SpawnShade();
        }

#pragma warning disable 649

        [SerializeField] private ShadeBrain shadeBrain;
        [SerializeField] private float shadeTimeoutTime;

#pragma warning restore 649

    }

}