using System;
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
        [SerializeField] private TileCollection wallBottomHalfTiles;
        [SerializeField] private TileCollection wallTopHalfTiles;
        [SerializeField] private TileCollection wallTopTiles;


        private readonly Rng rng = RngFromSeed(0);

        #endregion

        #region Methods

        internal Tile GetGroundTile() => 
            groundTiles.GetTile(rng);


        internal Tile GetWallTileFor(WallData wallData) =>
            GetTileCollectionFor(wallData.Type).GetTile(rng);

        private TileCollection GetTileCollectionFor(WallType wallType)
        {
            switch (wallType)
            {
                case WallType.TopHalf: return wallTopHalfTiles;
                case WallType.BottomHalf: return wallBottomHalfTiles;
                case WallType.Top: return wallTopTiles;
                default: throw new Exception("Invalid wall-type!");
            }
        }

        #endregion

    }

}