using AChildsCourage.Game.Shade.Navigation;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    public readonly struct GroundTile
    {
        
        public AoiIndex AoiIndex { get; }

        public TilePosition Position { get; }


        public GroundTile(AoiIndex aoiIndex, TilePosition position)
        {
            Position = position;
            AoiIndex = aoiIndex;
        }

    }

}