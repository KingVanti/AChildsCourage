using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class StaticObjectLayerEntity : TileLayerEntity
    {

        #region Fields

        [SerializeField] private Tile tile;

        #endregion

        #region Methods

        public void PlaceAll(IEnumerable<StaticObjectData> staticObjects)
        {
            Clear();

            foreach (var staticObject in staticObjects) Place(staticObject);
        }

        private void Place(StaticObjectData groundTile) => PlaceTileAt(tile, groundTile.Position);


        public void PlaceAt(Vector2Int position) => PlaceTileAt(tile, position);


        public StaticObjectData[] ReadAll() =>
            GetTiles()
                .Select(t => new StaticObjectData(t.Position))
                .ToArray();

        #endregion

    }

}