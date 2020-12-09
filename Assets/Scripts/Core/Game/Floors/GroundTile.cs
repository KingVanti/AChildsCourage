using AChildsCourage.Game.Shade.Navigation;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    public readonly struct GroundTile
    {

        #region Properties

        public AoiIndex AoiIndex { get; }

        public TilePosition Position { get; }

        #endregion

        #region Constructors

        public GroundTile(AoiIndex aoiIndex, TilePosition position)
        {
            Position = position;
            AoiIndex = aoiIndex;
        }

        #endregion

    }

}