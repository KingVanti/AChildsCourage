using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorBuilding
    {

        private static Floor GetFloor(this FloorBuilder builder)
        {
            Wall ToWall(TilePosition wallPosition) => builder.ToWall(wallPosition);

            var groundTilePosition = builder.GroundTilePositions;
            var walls =
                builder.WallTilePositions
                .Select(ToWall);

            return new Floor(groundTilePosition, walls);
        }

        private static Wall ToWall(this FloorBuilder builder, TilePosition wallPosition)
        {
            bool isSide = builder.GroundTilePositions.Contains(wallPosition + new TileOffset(0, -1)) || builder.GroundTilePositions.Contains(wallPosition + new TileOffset(0, -2));

            return new Wall(wallPosition, isSide ? WallType.Side : WallType.Top);
        }

    }

}