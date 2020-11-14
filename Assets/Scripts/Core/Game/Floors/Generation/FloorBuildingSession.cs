﻿using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors.Generation
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
            var filteredWallPositions =
                GetUnfilteredWallPositions()
                .Where(IsEmpty);

            foreach (var wallPosition in filteredWallPositions)
                wallTilePositions.Add(wallPosition);
        }

        private IEnumerable<TilePosition> GetUnfilteredWallPositions()
        {
            foreach (var groundPosition in GroundTilePositions)
                for (var dX = -1; dX <= 1; dX++)
                    for (var dY = -1; dY <= 3; dY++)
                        if (dX != 0 || dY != 0)
                            yield return groundPosition + new TileOffset(dX, dY);
        }

        private bool IsEmpty(TilePosition position)
        {
            return !groundTilePositions.Contains(position);
        }


        private Wall ToWall(TilePosition wallPosition)
        {
            bool isSide = groundTilePositions.Contains(wallPosition + new TileOffset(0, -1)) || groundTilePositions.Contains(wallPosition + new TileOffset(0, -2));

            return new Wall(wallPosition, isSide ? WallType.Side : WallType.Top);
        }

        #endregion

    }

}