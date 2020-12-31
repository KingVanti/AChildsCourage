using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using static AChildsCourage.Game.MOldFloorGenerating.MFloorBuilder;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.F;

namespace AChildsCourage.Game
{

    public static partial class MOldFloorGenerating
    {

        public static class MWallGenerating
        {

            private const int WallHeight = 2;


            public static FloorBuilder GenerateWalls(FloorBuilder floor)
            {
                var groundPositions = GetAllGroundPositions(floor);

                return Take(groundPositions)
                       .Map(GenerateWallPositions)
                       .Map(wallPositions => GetWalls(wallPositions, pos => CreateWall(pos, groundPositions)))
                       .Aggregate(floor, PlaceWall);
            }

            private static ImmutableHashSet<TilePosition> GetAllGroundPositions(FloorBuilder floor) =>
                floor
                    .Rooms
                    .SelectMany(r => r.GroundTiles)
                    .Select(t => t.Position)
                    .ToImmutableHashSet();

            private static ImmutableHashSet<TilePosition> GenerateWallPositions(ImmutableHashSet<TilePosition> groundPositions) =>
                groundPositions
                    .SelectMany(GetSurroundingWallPositions)
                    .Where(position => !groundPositions.Contains(position))
                    .ToImmutableHashSet();

            private static IEnumerable<TilePosition> GetSurroundingWallPositions(TilePosition groundPosition)
            {
                for (var dX = -1; dX <= 1; dX++)
                    for (var dY = -1; dY <= 3; dY++)
                        if (dX != 0 || dY != 0)
                            yield return groundPosition.Map(OffsetBy, new TileOffset(dX, dY));
            }

            private static IEnumerable<Wall> GetWalls(IEnumerable<TilePosition> wallPositions, Func<TilePosition, Wall> toWall) =>
                wallPositions
                    .Select(toWall);

            private static Wall CreateWall(TilePosition wallPosition, ImmutableHashSet<TilePosition> groundPositions)
            {
                var wallType = HasGroundBelow(wallPosition, groundPositions)
                    ? WallType.Side
                    : WallType.Top;

                return new Wall(wallPosition, wallType);
            }

            private static bool HasGroundBelow(TilePosition wallPosition, ImmutableHashSet<TilePosition> groundPositions) =>
                GetCheckGroundPositions(wallPosition)
                    .Any(groundPositions.Contains);

            private static IEnumerable<TilePosition> GetCheckGroundPositions(TilePosition wallPosition) =>
                GetGroundOffsets()
                    .Select(offset => wallPosition.Map(OffsetBy, offset));

            private static IEnumerable<TileOffset> GetGroundOffsets() =>
                Enumerable.Range(-WallHeight, WallHeight)
                          .Select(y => new TileOffset(0, y));

            private static FloorBuilder PlaceWall(FloorBuilder floor, Wall wall) =>
                new FloorBuilder(
                                 floor.Walls.Add(wall),
                                 floor.Rooms);

        }

    }

}