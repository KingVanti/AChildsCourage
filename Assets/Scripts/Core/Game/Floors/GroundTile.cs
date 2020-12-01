using AChildsCourage.Game.Monsters.Navigation;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    public readonly struct GroundTile
    {

        #region Properties

        public AOIIndex AOIIndex { get; }

        public TilePosition Position { get; }

        #endregion

        #region Constructors

        public GroundTile(AOIIndex aoiIndex, TilePosition position)
        {
            Position = position;
            AOIIndex = aoiIndex;
        }

        #endregion

    }

}