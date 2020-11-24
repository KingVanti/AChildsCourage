using AChildsCourage.Game.Floors;
using Ninject;
using Ninject.Extensions.Unity;
using Ninject.Parameters;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        private IRNG rng;

        #endregion

        #region Properties

        [AutoInject] public IKernel Kernel { set { rng = value.Get<IRNG>(new ConstructorArgument("seed", 0, true)); } }

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