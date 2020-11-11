using AChildsCourage.Game.Floors.Persistance;
using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class GridManager : MonoBehaviour
    {

        #region Fields

        [SerializeField] private TileTypeLayer groundLayer;
        [SerializeField] private TileTypeLayer smallCourageLayer;
        [SerializeField] private TileTypeLayer bigCourageLayer;
        [SerializeField] private TileTypeLayer itemLayer;

        #endregion

        #region Methods

        public void PlaceTiles(RoomTiles roomTiles)
        {
            groundLayer.PlaceTilesAt(roomTiles.GroundPositions);
            smallCourageLayer.PlaceTilesAt(roomTiles.SmallCouragePositions);
            bigCourageLayer.PlaceTilesAt(roomTiles.BigCouragePositions);
            itemLayer.PlaceTilesAt(roomTiles.ItemPositions);
        }

        #endregion

    }

}