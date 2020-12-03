using System.Collections.Immutable;
using AChildsCourage.Game.Monsters.Navigation;

namespace AChildsCourage.Game.Floors
{

    public static class MRoom
    {

        public static Room EmptyRoom(AOIIndex aoiIndex) =>
            new Room(
                aoiIndex,
                ImmutableHashSet<GroundTile>.Empty);

        public readonly struct Room
        {

            public AOIIndex AoiIndex { get; }

            public ImmutableHashSet<GroundTile> GroundTiles { get; }
            
            public Room(AOIIndex aoiIndex, ImmutableHashSet<GroundTile> groundTiles)
            {
                AoiIndex = aoiIndex;
                GroundTiles = groundTiles;
            }

        }

    }

}