using AChildsCourage.Game.Floors;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.Rng;

namespace AChildsCourage.Game
{

    public class TileRepositoryEntity : MonoBehaviour
    {

        #region Fields

        [SerializeField] private TileCollection groundTiles;
        [SerializeField] private TileCollection wallSideTiles;
        [SerializeField] private TileCollection wallTopTiles;


        private readonly Rng rng = RngFromSeed(0);

        #endregion

        #region Methods

        public Tile GetGroundTile() => groundTiles.GetTile(rng);


        public Tile GetWallTileFor(WallData wallData) =>
            wallData.Type == WallType.Side ? wallSideTiles.GetTile(rng) : wallTopTiles.GetTile(rng);

        #endregion

    }

}