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

        public void PlaceAll(IEnumerable<SerializedStaticObject> staticObjects)
        {
            Clear();

            foreach (var staticObject in staticObjects) Place(staticObject);
        }

        private void Place(SerializedStaticObject groundTile) => PlaceTileAt(tile, groundTile.Position);


        public void PlaceAt(Vector2Int position) => PlaceTileAt(tile, position);


        public SerializedStaticObject[] ReadAll() =>
            GetTiles()
                .Select(t => new SerializedStaticObject(t.Position))
                .ToArray();

        #endregion

    }

}