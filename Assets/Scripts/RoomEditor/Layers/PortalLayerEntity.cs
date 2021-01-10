using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class PortalLayerEntity : TileLayerEntity
    {

        #region Fields

        [SerializeField] private Tile tile;

        #endregion

        #region Methods

        public void PlaceAll(IEnumerable<SerializedPortal> portals)
        {
            Clear();

            foreach (var portal in portals) Place(portal);
        }

        private void Place(SerializedPortal portal) => PlaceTileAt(tile, portal.Position);


        public void PlaceAt(Vector2Int position) => PlaceTileAt(tile, position);


        public SerializedPortal[] ReadAll() =>
            GetTiles()
                .Select(t => new SerializedPortal(t.Position))
                .ToArray();

        #endregion

    }

}