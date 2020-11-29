using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistance;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class GroundTileLayer : TileLayer
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Tile tile;

#pragma warning restore 649

        #endregion

        #region Methods

        public void PlaceAll(GroundTileData[] groundTiles)
        {
            Clear();

            foreach (var groundTile in groundTiles)
                Place(groundTile);
        }

        private void Place(GroundTileData groundTile)
        {
            PlaceTileAt(tile, groundTile.Position);
        }


        public void PlaceAt(Vector2Int position)
        {
            PlaceTileAt(tile, position);
        }


        public GroundTileData[] ReadAll()
        {
            return
                GetTiles()
                    .Select(t => new GroundTileData(t.Position, 0, 0))
                    .ToArray();
        }

        #endregion

    }

}