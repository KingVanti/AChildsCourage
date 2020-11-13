using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public class Floor
    {

        #region Fields

        private readonly HashSet<TilePosition> groundTilePositions = new HashSet<TilePosition>();
        private readonly HashSet<TilePosition> wallTilePositions = new HashSet<TilePosition>();

        #endregion

        #region Properties

        public int GroundTileCount { get { return groundTilePositions.Count; } }


        public int WallTileCount { get { return wallTilePositions.Count; } }


        public IEnumerable<TilePosition> GroundTilePositions { get { return groundTilePositions; } }


        public IEnumerable<TilePosition> WallTilePositions { get { return wallTilePositions; } }

        #endregion

        #region Methods

        public void PlaceGround(GroundTile groundTile, TileOffset tileOffset)
        {
            var position = groundTile.Position + tileOffset;

            if (!groundTilePositions.Contains(position))
                groundTilePositions.Add(position);
        }


        public void GenerateWalls()
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

        #endregion

    }

}