using AChildsCourage.Game.Floors.Persistance;
using System;
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


        public void PlaceTileOfType(Vector2Int position, TileType tileType)
        {
            var layer = GetLayerForTileOfType(tileType);

            layer.PlaceTileAt(position);
        }


        public void DeleteTileOfType(Vector2Int position, TileType tileType)
        {
            var layer = GetLayerForTileOfType(tileType);

            layer.DeleteTileAt(position);
        }


        private TileTypeLayer GetLayerForTileOfType(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Ground:
                    return groundLayer;
            }

            throw new Exception("Invalid tile type!");
        }

        #endregion

    }

}