using AChildsCourage.Game.Floors.RoomPersistance;
using System;
using System.Linq;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class GroundBuilding
    {

        internal static GroundBuilder GetDefault()
        {
            return BuildGroundTiles;
        }


        internal static FloorInProgress BuildGroundTiles(FloorInProgress floor, GroundTileData[] tiles, TileTransformer transformer)
        {
            Func<GroundTileData, GroundTileData> transformed = tile => TransformGroundTile(tile, transformer);
            Action<GroundTileData> place = tile => PlaceGroundTile(tile, floor);

            return
                Pipe(tiles)
                .Select(transformed)
                .AllInto(place)
                .FinallyReturn(floor);
        }

        internal static GroundTileData TransformGroundTile(GroundTileData groundTile, TileTransformer transformer)
        {
            var newPosition = transformer(groundTile.Position);

            return groundTile.With(newPosition);
        }

        internal static GroundTileData With(this GroundTileData groundTile, TilePosition position)
        {
            return new GroundTileData(position);
        }

        internal static void PlaceGroundTile(GroundTileData groundTile, FloorInProgress floor)
        {
            floor.GroundPositions.Add(groundTile.Position);
        }

    }

}