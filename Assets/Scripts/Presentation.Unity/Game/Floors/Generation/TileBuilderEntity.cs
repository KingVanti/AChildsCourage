using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.Game.Floors.Generation
{

    public class TileBuilderEntity : MonoBehaviour
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

        [AutoInject] public ITileBuilder TileBuilder { set { BindTo(value); } }

        #endregion

        #region Methods

        private void BindTo(ITileBuilder tileBuilder)
        {
            tileBuilder.OnGroundPlaced += (_, e) => OnGroundPlaced(e);
            tileBuilder.OnWallPlaced += (_, e) => OnWallPlaced(e);
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