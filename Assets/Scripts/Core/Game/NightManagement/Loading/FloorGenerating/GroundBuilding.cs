using AChildsCourage.Game.Floors;
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


        internal static FloorBuilder BuildGroundTiles(FloorBuilder builder, Tiles<GroundTile> tiles, TileTransformer transformer)
        {
            Func<GroundTile, GroundTile> transformed = tile => TransformGroundTile(tile, transformer);
            Action<GroundTile> place = tile => PlaceGroundTile(tile, builder);

            return
                Pipe(tiles)
                .Select(transformed)
                .AllInto(place)
                .FinallyReturn(builder);
        }

        internal static GroundTile TransformGroundTile(GroundTile groundTile, TileTransformer transformer)
        {
            var newPosition = transformer(groundTile.Position);

            return groundTile.With(newPosition);
        }

        internal static GroundTile With(this GroundTile groundTile, TilePosition position)
        {
            return new GroundTile(position, groundTile.DistanceToWall, groundTile.AOIIndex);
        }

        internal static void PlaceGroundTile(GroundTile groundTile, FloorBuilder builder)
        {
            builder.GroundPositions.Add(groundTile.Position);
        }

    }

}