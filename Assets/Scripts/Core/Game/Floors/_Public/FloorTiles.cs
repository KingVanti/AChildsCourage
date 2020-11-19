using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    public class FloorTiles
    {

        #region Properties

        internal IEnumerable<TilePosition> GroundTilePositions { get; }

        internal IEnumerable<Wall> Walls { get; }

        internal IEnumerable<TilePosition> CourageOrbPositions { get; }

        #endregion

        #region Constructors

        public FloorTiles(IEnumerable<TilePosition> groundTilePositions, IEnumerable<Wall> walls, IEnumerable<TilePosition> courageOrbPositions)
        {
            GroundTilePositions = new HashSet<TilePosition>(groundTilePositions);
            Walls = new HashSet<Wall>(walls);
            CourageOrbPositions = new HashSet<TilePosition>(courageOrbPositions);
        }


        #endregion

    }

}