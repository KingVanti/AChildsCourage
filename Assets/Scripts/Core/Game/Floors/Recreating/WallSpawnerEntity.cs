using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    public class WallSpawnerEntity : MonoBehaviour
    {

        [FindComponent] private Tilemap wallTilemap;

        [FindInScene] private TileRepositoryEntity tileRepository;


        internal void Spawn(TilePosition position, WallData wallData)
        {
            var tile = tileRepository.GetWallTileFor(wallData);

            wallTilemap.SetTile(position.Map(ToVector3Int), tile);
        }

    }

}