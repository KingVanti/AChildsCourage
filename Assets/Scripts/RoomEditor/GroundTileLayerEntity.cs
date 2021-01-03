using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class GroundTileLayerEntity : TileLayerEntity
    {

        #region Fields

        [SerializeField] private Tile tile;

        #endregion

        #region Methods

        public void PlaceAll(IEnumerable<SerializedGroundTile> groundTiles)
        {
            Clear();

            foreach (var groundTile in groundTiles) Place(groundTile);
        }

        private void Place(SerializedGroundTile serializedGroundTile) => PlaceTileAt(tile, serializedGroundTile.Position);


        public void PlaceAt(Vector2Int position) => PlaceTileAt(tile, position);


        public SerializedGroundTile[] ReadAll() =>
            GetTiles()
                .Select(t => new SerializedGroundTile(t.Position))
                .ToArray();

        #endregion

    }

}