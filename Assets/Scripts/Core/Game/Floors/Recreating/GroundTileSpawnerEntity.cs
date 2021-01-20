using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    public class GroundTileSpawnerEntity : MonoBehaviour
    {

        [FindComponent] private Tilemap groundTilemap;

        [FindInScene] private TileRepositoryEntity tileRepository;


        internal void Spawn(TilePosition position, GroundTileData _)
        {
            var tile = tileRepository.GetGroundTile();

            groundTilemap.SetTile(position.Map(ToVector3Int), tile);
        }

    }

}