using AChildsCourage.Game.Floors;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game
{

    [UseDI]
    public class TileRepository : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private TileCollection groundTiles;
        [SerializeField] private TileCollection wallSideTiles;
        [SerializeField] private TileCollection wallTopTiles;

#pragma warning restore 649

        private readonly CreateRNG createRng = FromSeed(0);

        #endregion

        #region Methods

        public Tile GetGroundTile() => groundTiles.GetTile(createRng);


        public Tile GetWallTileFor(Wall wall) =>
            wall.Type == WallType.Side
                ? wallSideTiles.GetTile(createRng)
                : wallTopTiles.GetTile(createRng);

        #endregion

    }

}