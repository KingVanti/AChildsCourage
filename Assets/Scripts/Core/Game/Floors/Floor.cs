using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    public class Floor
    {

        #region Constructors

        public Floor(IEnumerable<GroundTile> groundTiles, IEnumerable<Wall> walls, IEnumerable<CouragePickup> couragePickups, IEnumerable<ItemPickup> itemPickups)
        {
            GroundTiles = new HashSet<GroundTile>(groundTiles);
            Walls = new HashSet<Wall>(walls);
            CouragePickups = new HashSet<CouragePickup>(couragePickups);
            ItemPickups = new HashSet<ItemPickup>(itemPickups);
        }

        #endregion

        #region Properties

        public HashSet<GroundTile> GroundTiles { get; }

        public HashSet<Wall> Walls { get; }

        public HashSet<CouragePickup> CouragePickups { get; }

        public HashSet<ItemPickup> ItemPickups { get; }

        #endregion

    }

}