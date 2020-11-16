using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static FloorTiles GetFloor(this FloorTilesBuilder builder)
        {
            Wall ToWall(TilePosition wallPosition) => builder.ToWall(wallPosition);

            var groundTilePosition = builder.GroundPositions;
            var walls =
                builder.WallTilePositions
                .Select(ToWall);

            return new FloorTiles(groundTilePosition, walls);
        }

        private static Wall ToWall(this FloorTilesBuilder builder, TilePosition wallPosition)
        {
            bool isSide = builder.GroundPositions.Contains(wallPosition + new TileOffset(0, -1)) || builder.GroundPositions.Contains(wallPosition + new TileOffset(0, -2));

            return new Wall(wallPosition, isSide ? WallType.Side : WallType.Top);
        }

    }

}