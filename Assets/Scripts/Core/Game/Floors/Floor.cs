using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    internal class Floor
    {

        #region Fields

        private readonly HashSet<TilePosition> groundTilePositions ;
        private readonly HashSet<Wall> walls;

        #endregion

        #region Properties

        internal int GroundTileCount { get { return groundTilePositions.Count; } }


        internal int WallCount { get { return walls.Count; } }


        internal IEnumerable<TilePosition> GroundTilePositions { get { return groundTilePositions; } }


        internal IEnumerable<Wall> Walls { get { return walls; } }

        #endregion

        #region Constructors

        internal Floor(IEnumerable<TilePosition> groundTilePositions, IEnumerable<Wall> walls)
        {
            this.groundTilePositions = new HashSet<TilePosition>(groundTilePositions);
            this.walls = new HashSet<Wall>(walls);
        }

        #endregion

    }

}