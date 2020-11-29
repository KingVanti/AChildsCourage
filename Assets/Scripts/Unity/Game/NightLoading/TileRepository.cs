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

        private readonly RNGSource rng = FromSeed(0);

        #endregion

        #region Methods

        public Tile GetGroundTile()
        {
            return groundTiles.GetTile(rng);
        }


        public Tile GetWallTileFor(Wall wall)
        {
            return
                wall.Type == WallType.Side
                    ? wallSideTiles.GetTile(rng)
                    : wallTopTiles.GetTile(rng);
        }

        #endregion

    }

}