﻿using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static void GenerateWalls(this FloorTilesBuilder builder)
        {
            bool IsEmpty(TilePosition position) => builder.IsEmpty(position);

            var wallPositions =
                builder.GroundPositions
                .SelectMany(GetUnfilteredWallPositions)
                .Where(IsEmpty);

            foreach (var wallPosition in wallPositions)
                builder.WallPositions.Add(wallPosition);
        }

        private static IEnumerable<TilePosition> GetUnfilteredWallPositions(TilePosition groundPosition)
        {
            for (var dX = -1; dX <= 1; dX++)
                for (var dY = -1; dY <= 3; dY++)
                    if (dX != 0 || dY != 0)
                        yield return groundPosition + new TileOffset(dX, dY);
        }

        private static bool IsEmpty(this FloorTilesBuilder builder, TilePosition position)
        {
            return builder.GroundPositions.Contains(position);
        }

    }

}