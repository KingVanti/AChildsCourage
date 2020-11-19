using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private const int WallHeight = 2;

        private static FloorTiles GetFloor(this FloorTilesBuilder builder, IRNG rng)
        {
            var groundTilePosition = builder.GroundPositions;
            var walls = builder.GetWalls();
            var courageOrbPositions = builder.ChooseCourageOrbPositions(rng);

            return new FloorTiles(groundTilePosition, walls, courageOrbPositions);
        }

        private static IEnumerable<Wall> GetWalls(this FloorTilesBuilder builder)
        {
            Func<TilePosition, bool> hasGroundBelow = pos => HasGroundBelow(pos, builder.HasGroundAt);
            Func<TilePosition, Wall> toWall = pos => ToWall(pos, hasGroundBelow(pos));

            return GetWalls(builder.WallPositions, toWall);
        }

        private static IEnumerable<Wall> GetWalls(IEnumerable<TilePosition> wallPositions, Func<TilePosition, Wall> toWall)
        {
            return
                wallPositions
                .Select(toWall);
        }

        internal static bool HasGroundBelow(TilePosition wallPosition, Func<TilePosition, bool> hasGroundAt)
        {
            var p = GetCheckGroundPositions(wallPosition).ToArray();
            return
                GetCheckGroundPositions(wallPosition)
                .Any(hasGroundAt);
        }

        internal static Wall ToWall(TilePosition wallPosition, bool hasGroundBelow)
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

        internal static bool HasGroundAt(this FloorTilesBuilder builder, TilePosition position)
        {
            return builder.GroundPositions.Contains(position);
        }

    }

}