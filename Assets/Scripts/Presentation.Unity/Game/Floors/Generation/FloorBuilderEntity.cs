using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.Game.Floors.Generation
{

    public class FloorBuilderEntity : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Tile floorTile;
        [SerializeField] private Tilemap floorTilemap;

#pragma warning restore 649

        #endregion

        #region Properties

        [AutoInject] public IFloorBuilder FloorBuilder { set { BindTo(value); } }

        #endregion

        #region Methods

        private void BindTo(IFloorBuilder floorBuilder)
        {
            floorBuilder.OnFloorPlaced += (_, e) => OnFloorPlaced(e);
            floorBuilder.PlaceFloor(new TilePosition(1, 1), new RoomBuildingSession());
        }

        private void OnFloorPlaced(FloorPlacedEventArgs eventArgs)
        {
            var tile = floorTile;
            var position = eventArgs.Position.ToVector3Int();

            floorTilemap.SetTile(position, tile);
        }

        #endregion

    }

}