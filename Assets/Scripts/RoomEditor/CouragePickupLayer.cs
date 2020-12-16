using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.RoomEditor
{

    public class CouragePickupLayer : TileLayer
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private Tile courageSparkTile;
        [SerializeField] private Tile courageOrbTile;

#pragma warning restore 649

        #endregion

        #region Methods

        public void PlaceAll(IEnumerable<CouragePickupData> couragePickups)
        {
            Clear();

            foreach (var couragePickup in couragePickups) Place(couragePickup);
        }

        private void Place(CouragePickupData couragePickup)
        {
            var tile = GetTileFor(couragePickup.Variant);

            PlaceTileAt(tile, couragePickup.Position);
        }


        public void PlaceAt(Vector2Int position, CourageVariant variant)
        {
            var tile = GetTileFor(variant);

            PlaceTileAt(tile, position);
        }


        public CouragePickupData[] ReadAll() =>
            GetTiles()
                .Select(ToPickup)
                .ToArray();

        private CouragePickupData ToPickup(TileAtPos tileAtPos)
        {
            var position = tileAtPos.Position;
            var variant = GetVariantOf(tileAtPos.Tile);

            return new CouragePickupData(position, variant);
        }

        private static CourageVariant GetVariantOf(Tile tile) => tile.name.Contains("Orb") ? CourageVariant.Orb : CourageVariant.Spark;


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