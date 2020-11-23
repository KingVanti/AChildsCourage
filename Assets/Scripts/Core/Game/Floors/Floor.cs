using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    public class Floor
    {

        #region Properties

        internal HashSet<GroundTile> GroundTiles { get; }

        internal HashSet<Wall> Walls { get; }

        internal HashSet<CouragePickup> CouragePickups { get; }

        #endregion

        #region Constructors

        public Floor(IEnumerable<GroundTile> groundTiles, IEnumerable<Wall> walls, IEnumerable<CouragePickup> couragePickups)
        {
            GroundTiles = new HashSet<GroundTile>(groundTiles);
            Walls = new HashSet<Wall>(walls);
            CouragePickups = new HashSet<CouragePickup>(couragePickups);
        }


        #endregion

    }

}