using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace AChildsCourage.RoomEditor
{

    public class CouragePickupLayerEntity : TileLayerEntity
    {

        #region Fields

        [SerializeField] private Tile courageSparkTile;
        [SerializeField] private Tile courageOrbTile;

        #endregion

        #region Methods

        public void PlaceAll(IEnumerable<SerializedCouragePickup> couragePickups)
        {
            Clear();

            foreach (var couragePickup in couragePickups) Place(couragePickup);
        }

        private void Place(SerializedCouragePickup serializedCouragePickup)
        {
            var tile = GetTileFor(serializedCouragePickup.Variant);

            PlaceTileAt(tile, serializedCouragePickup.Position);
        }


        public void PlaceAt(Vector2Int position, CourageVariant variant)
        {
            var tile = GetTileFor(variant);

            PlaceTileAt(tile, position);
        }


        public SerializedCouragePickup[] ReadAll() =>
            GetTiles()
                .Select(ToPickup)
                .ToArray();

        private static SerializedCouragePickup ToPickup(TileAtPos tileAtPos)
        {
            var position = tileAtPos.Position;
            var variant = GetVariantOf(tileAtPos.Tile);

            return new SerializedCouragePickup(position, variant);
        }

        private static CourageVariant GetVariantOf(Object tile) => tile.name.Contains("Orb") ? CourageVariant.Orb : CourageVariant.Spark;


        private Tile GetTileFor(CourageVariant variant)
        {
            switch (variant)
            {
                case CourageVariant.Spark: return courageSparkTile;
                case CourageVariant.Orb: return courageOrbTile;
                default: throw new Exception("Invalid variant!");
            }
        }

        #endregion

    }

}