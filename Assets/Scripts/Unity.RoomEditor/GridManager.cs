using AChildsCourage.Game.Floors.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class GridManager : MonoBehaviour
    {

        #region Fields

        [SerializeField] private TileTypeLayer[] layers;

        #endregion

        #region Methods

        public void PlaceTiles(RoomTiles roomTiles)
        {
            GetLayerForTileOfType(TileType.Ground).PlaceTilesAt(roomTiles.GroundPositions);
            GetLayerForTileOfType(TileType.Item).PlaceTilesAt(roomTiles.ItemPositions);
            GetLayerForTileOfType(TileType.CourageSmall).PlaceTilesAt(roomTiles.SmallCouragePositions);
            GetLayerForTileOfType(TileType.CourageBig).PlaceTilesAt(roomTiles.BigCouragePositions);
        }


        public void PlaceTileOfType(Vector2Int position, TileType tileType)
        {
            var category = GetTileTypeCategory(tileType);

            foreach (var layer in GetAllLayersOfCategory(category))
                if (layer.Type == tileType)
                    layer.PlaceTileAt(position);
                else
                    layer.DeleteTileAt(position);
        }


        public void DeleteTileOfType(Vector2Int position, TileType tileType)
        {
            var category = GetTileTypeCategory(tileType);

            foreach (var layer in GetAllLayersOfCategory(category))
                layer.DeleteTileAt(position);
        }


        private TileTypeLayer GetLayerForTileOfType(TileType tileType)
        {
            return layers.Where(l => l.Type == tileType).FirstOrDefault();
        }

        private IEnumerable<TileTypeLayer> GetAllLayersOfCategory(TileTypeCategory category)
        {
            return layers.Where(l => l.Category == category);
        }

        private TileTypeCategory GetTileTypeCategory(TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Ground:
                    return TileTypeCategory.Ground;
                case TileType.Item:
                case TileType.CourageSmall:
                case TileType.CourageBig:
                    return TileTypeCategory.Static;
            }

            throw new Exception("Invalid tile type!");
        }

        #endregion

    }

}