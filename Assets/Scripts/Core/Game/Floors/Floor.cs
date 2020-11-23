using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    public class Floor
    {

        #region Properties

        internal HashSet<GroundTile> GroundTiles { get; }

        internal HashSet<Wall> Walls { get; }

        internal HashSet<TilePosition> CourageOrbPositions { get; }

        #endregion

        #region Constructors

        public Floor(IEnumerable<GroundTile> groundTiles, IEnumerable<Wall> walls, IEnumerable<TilePosition> courageOrbPositions)
        {
            GroundTiles = new HashSet<GroundTile>(groundTiles);
            Walls = new HashSet<Wall>(walls);
            CourageOrbPositions = new HashSet<TilePosition>(courageOrbPositions);
        }


        #endregion

    }

}