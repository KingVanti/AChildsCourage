using System.Collections.Generic;
using System.Linq;

using static AChildsCourage.Game.Floors.Building.FloorBuildingModule;

namespace AChildsCourage.Game.Floors.Building
{

    internal class FloorBuildingSession
    {

        #region Fields

        private readonly HashSet<TilePosition> groundTilePositions = new HashSet<TilePosition>();
        private readonly HashSet<TilePosition> wallTilePositions = new HashSet<TilePosition>();

        #endregion

        #region Properties

        internal int WallCount { get { return wallTilePositions.Count; } }


        internal IEnumerable<TilePosition> GroundTilePositions { get { return groundTilePositions; } }


        internal IEnumerable<Wall> Walls { get { return wallTilePositions.Select(ToWall); } }

        #endregion

        #region Methods

        internal Floor BuildFloor()
        {
            return new Floor(GroundTilePositions, Walls);
        }


        internal void PlaceGround(GroundTile groundTile, TileOffset tileOffset)
        {
            var position = groundTile.Position + tileOffset;

            if (!groundTilePositions.Contains(position))
                groundTilePositions.Add(position);
        }


        internal void GenerateWalls()
        {
            foreach (var wallPosition in GenerateWallsFor(groundTilePositions))
                wallTilePositions.Add(wallPosition);
        }


        private Wall ToWall(TilePosition wallPosition)
        {
            bool isSide = groundTilePositions.Contains(wallPosition + new TileOffset(0, -1)) || groundTilePositions.Contains(wallPosition + new TileOffset(0, -2));

            return new Wall(wallPosition, isSide ? WallType.Side : WallType.Top);
        }

        #endregion

    }

}