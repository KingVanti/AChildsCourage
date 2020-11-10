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

        public void PlaceTilesFor(RoomData roomData)
        {
            groundLayer.PlaceTilesAt(roomData.GroundPositions);
            smallCourageLayer.PlaceTilesAt(roomData.SmallCouragePositions);
            bigCourageLayer.PlaceTilesAt(roomData.BigCouragePositions);
            itemLayer.PlaceTilesAt(roomData.ItemPositions);
        }

        #endregion

    }

}