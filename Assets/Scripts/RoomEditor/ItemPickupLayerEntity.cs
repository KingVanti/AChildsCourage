using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class ItemPickupLayerEntity : TileLayerEntity
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Tile itemTile;

#pragma warning restore 649

        #endregion

        #region Methods

        public void PlaceAll(IEnumerable<ItemPickupData> itemPickups)
        {
            Clear();

            foreach (var itemPickup in itemPickups) Place(itemPickup);
        }

        private void Place(ItemPickupData itemPickup) => PlaceTileAt(itemTile, itemPickup.Position);


        public void PlaceAt(Vector2Int position) => PlaceTileAt(itemTile, position);


        public ItemPickupData[] ReadAll() =>
            GetTiles()
                .Select(ToPickup)
                .ToArray();

        private static ItemPickupData ToPickup(TileAtPos tileAtPos)
        {
            var position = tileAtPos.Position;

            return new ItemPickupData(position);
        }

        #endregion

    }

}