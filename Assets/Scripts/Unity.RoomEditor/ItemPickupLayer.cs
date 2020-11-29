using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistance;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class ItemPickupLayer : TileLayer
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Tile itemTile;

#pragma warning restore 649

        #endregion

        #region Methods

        public void PlaceAll(ItemPickupData[] itemPickups)
        {
            Clear();

            foreach (var itemPickup in itemPickups)
                Place(itemPickup);
        }

        private void Place(ItemPickupData itemPickup)
        {
            PlaceTileAt(itemTile, itemPickup.Position);
        }


        public void PlaceAt(Vector2Int position)
        {
            PlaceTileAt(itemTile, position);
        }


        public ItemPickupData[] ReadAll()
        {
            return
                GetTiles()
                    .Select(ToPickup)
                    .ToArray();
        }

        private ItemPickupData ToPickup(TileAtPos tileAtPos)
        {
            var position = tileAtPos.Position;

            return new ItemPickupData(position);
        }

        #endregion

    }

}