using System.Collections.Immutable;
using AChildsCourage.Game.Monsters.Navigation;

namespace AChildsCourage.Game.Floors
{

    public static class MRoom
    {

        public static Room EmptyRoom(AoiIndex aoiIndex) =>
            new Room(
                aoiIndex,
                ImmutableHashSet<GroundTile>.Empty);

        public readonly struct Room
        {

            public AoiIndex AoiIndex { get; }

            public ImmutableHashSet<GroundTile> GroundTiles { get; }
            
            public Room(AoiIndex aoiIndex, ImmutableHashSet<GroundTile> groundTiles)
            {
                AoiIndex = aoiIndex;
                GroundTiles = groundTiles;
            }

        }

    }

}