using AChildsCourage.Game.Floors;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.NightLoading
{

    public class TileRepository : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private TileCollection groundTiles;
        [SerializeField] private TileCollection wallSideTiles;
        [SerializeField] private TileCollection wallTopTiles;

#pragma warning restore 649

        private readonly CreateRNG _createRng = FromSeed(0);

        #endregion

        #region Methods

        public Tile GetGroundTile()
        {
            return groundTiles.GetTile(_createRng);
        }


        public Tile GetWallTileFor(Wall wall)
        {
            return
                wall.Type == WallType.Side
                    ? wallSideTiles.GetTile(_createRng)
                    : wallTopTiles.GetTile(_createRng);
        }

        #endregion

    }

}