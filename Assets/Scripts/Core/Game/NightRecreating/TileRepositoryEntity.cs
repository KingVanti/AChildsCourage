using AChildsCourage.Game.Floors;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    [UseDi]
    public class TileRepositoryEntity : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private TileCollection groundTiles;
        [SerializeField] private TileCollection wallSideTiles;
        [SerializeField] private TileCollection wallTopTiles;

#pragma warning restore 649

        private readonly CreateRng createRng = RngFromSeed(0);

        #endregion

        #region Methods

        public Tile GetGroundTile() => groundTiles.GetTile(createRng);


        public Tile GetWallTileFor(Wall wall) =>
            wall.Type == WallType.Side ? wallSideTiles.GetTile(createRng) : wallTopTiles.GetTile(createRng);

        #endregion

    }

}