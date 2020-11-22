using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using System.Linq;
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

        public void PlaceAll(CouragePickupData[] couragePickups)
        {
            Clear();

            foreach (var couragePickup in couragePickups)
                Place(couragePickup);
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


        public CouragePickupData[] ReadAll()
        {
            return
                GetTiles()
                .Select(ToPickup)
                .ToArray();
        }

        private CouragePickupData ToPickup(TileAtPos tileAtPos)
        {
            var position = tileAtPos.Position;
            var variant = GetVariantOf(tileAtPos.Tile);

            return new CouragePickupData(position, variant);
        }

        private CourageVariant GetVariantOf(Tile tile)
        {
            return tile.name.Contains("Orb") ? CourageVariant.Orb : CourageVariant.Spark;
        }


        private Tile GetTileFor(CourageVariant variant)
        {
            switch (variant)
            {
                case CourageVariant.Spark:
                    return courageSparkTile;
                case CourageVariant.Orb:
                    return courageOrbTile;
            }

            throw new System.Exception("Invalid variant!");
        }

        #endregion

    }

}