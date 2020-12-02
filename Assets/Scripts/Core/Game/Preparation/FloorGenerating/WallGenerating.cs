using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    internal static partial class FloorGenerating
    {

        internal const int WallHeight = 2;


        private static IEnumerable<Wall> GenerateWalls(FloorInProgress floor)
        {
            bool IsEmpty(TilePosition position)
            {
                return !floor.HasGroundAt(position);
            }

            Func<TilePosition, bool> hasGroundBelow = pos => HasGroundBelow(pos, floor.HasGroundAt);
            Func<TilePosition, Wall> toWall = pos => CreateWall(pos, hasGroundBelow(pos));

            return
                floor.GroundPositions
                     .GenerateWallPositionsFor(IsEmpty)
                     .IntoWith(GetWalls, toWall);
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
            return
                groundPositions
                    .SelectMany(GetSurroundingWallPositions)
                    .Where(isEmpty);
        }

        private static IEnumerable<Wall> GetWalls(IEnumerable<TilePosition> wallPositions, Func<TilePosition, Wall> toWall)
        {
            return wallPositions.Select(toWall);
        }

        internal static bool HasGroundBelow(TilePosition wallPosition, Func<TilePosition, bool> hasGroundAt)
        {
            var p = GetCheckGroundPositions(wallPosition)
                .ToArray();
            return
                GetCheckGroundPositions(wallPosition)
                    .Any(hasGroundAt);
        }

        internal static Wall CreateWall(TilePosition wallPosition, bool hasGroundBelow)
        {
            var wallType = hasGroundBelow ? WallType.Side : WallType.Top;

            return new Wall(wallPosition, wallType);
        }

        internal static IEnumerable<TilePosition> GetCheckGroundPositions(TilePosition wallPosition)
        {
            return
                GetGroundOffsets()
                    .Select(offset => wallPosition + offset);
        }

        internal static IEnumerable<TileOffset> GetGroundOffsets()
        {
            return
                Enumerable.Range(-WallHeight, WallHeight)
                          .Select(y => new TileOffset(0, y));
        }

        internal static bool HasGroundAt(this FloorInProgress floor, TilePosition position)
        {
            return floor.GroundPositions.Contains(position);
        }

    }

}