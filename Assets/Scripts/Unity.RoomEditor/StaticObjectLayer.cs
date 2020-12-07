using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class StaticObjectLayer : TileLayer
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Tile tile;

#pragma warning restore 649

        #endregion

        #region Methods

        public void PlaceAll(IEnumerable<StaticObjectData> staticObjects)
        {
            Clear();

            foreach (var staticObject in staticObjects)
                Place(staticObject);
        }

        private void Place(StaticObjectData groundTile)
        {
            PlaceTileAt(tile, groundTile.Position);
        }


        public void PlaceAt(Vector2Int position)
        {
            PlaceTileAt(tile, position);
        }


        public StaticObjectData[] ReadAll()
        {
            return GetTiles()
                   .Select(t => new StaticObjectData(t.Position))
                   .ToArray();
        }

        #endregion

    }

}