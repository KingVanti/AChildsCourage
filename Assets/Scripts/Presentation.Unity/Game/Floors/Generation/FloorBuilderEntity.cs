using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.Game.Floors.Generation
{

    public class FloorBuilderEntity : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Tile groundTile;
        [SerializeField] private Tile wallTile;
        [SerializeField] private Tilemap groundTilemap;
        [SerializeField] private Tilemap staticTilemap;

#pragma warning restore 649

        #endregion

        #region Properties

        [AutoInject] public IFloorBuilder FloorBuilder { set { BindTo(value); } }

        #endregion

        #region Methods

        private void BindTo(IFloorBuilder floorBuilder)
        {
            floorBuilder.OnGroundPlaced += (_, e) => OnGroundPlaced(e);
            floorBuilder.OnWallPlaced += (_, e) => OnWallPlaced(e);
        }

        private void OnGroundPlaced(GroundPlacedEventArgs eventArgs)
        {
            var tile = groundTile;
            var position = eventArgs.Position.ToVector3Int();

            groundTilemap.SetTile(position, tile);
        }

        private void OnWallPlaced(WallPlacedEventArgs eventArgs)
        {
            var tile = wallTile;
            var position = eventArgs.Position.ToVector3Int();

            staticTilemap.SetTile(position, tile);
        }

        #endregion

    }

}