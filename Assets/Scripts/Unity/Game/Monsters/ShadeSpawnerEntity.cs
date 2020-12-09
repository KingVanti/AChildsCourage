using UnityEngine;
using UnityEngine.Events;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters
{

    public class ShadeSpawnerEntity : MonoBehaviour
    {

        public UnityEvent onShadeSpawned;

#pragma warning disable 649

        [SerializeField] private Shade shade;

#pragma warning restore 649

        private TilePosition spawnTile;


        public void SpawnShade()
        {
            TeleportShadeToSpawn();
            onShadeSpawned.Invoke();
        }

        private void TeleportShadeToSpawn()
        {
            shade.transform.position = spawnTile.ToVector3();
        }


        public void OnFloorBuilt(Floor floor)
        {
            spawnTile = GetChunkCenter(floor.EndRoomChunkPosition);
        }

    }

}