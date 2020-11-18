using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static void GenerateWalls(this FloorTilesBuilder builder)
        {
            bool IsEmpty(TilePosition position) => !builder.HasGroundAt(position);

            void AddToWallPositions(TilePosition position) => _ = builder.WallPositions.Add(position);

            builder.GroundPositions
                .GenerateWallPositionsFor(IsEmpty)
                .ForEach(AddToWallPositions);
        }

        private static IEnumerable<TilePosition> GetSurroundingWallPositions(TilePosition groundPosition)
        {
            for (var dX = -1; dX <= 1; dX++)
                for (var dY = -1; dY <= 3; dY++)
                    if (dX != 0 || dY != 0)
                        yield return groundPosition + new TileOffset(dX, dY);
        }

        private static IEnumerable<TilePosition> GenerateWallPositionsFor(this IEnumerable<TilePosition> groundPositions, Func<TilePosition, bool> isEmpty)
        {
            return groundPositions
                .SelectMany(GetSurroundingWallPositions)
                .Where(isEmpty);
        }

    }

}