using System.Collections.Immutable;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Shade.Navigation;

namespace AChildsCourage.Game.Floors
{

    public static class MRoom
    {

        public static Room EmptyRoom(AoiIndex aoiIndex) =>
            new Room(
                aoiIndex,
                ImmutableHashSet<GroundTile>.Empty,
                ImmutableHashSet<StaticObject>.Empty,
                ImmutableHashSet<Rune>.Empty);

        public readonly struct Room
        {

            public AoiIndex AoiIndex { get; }

            public ImmutableHashSet<GroundTile> GroundTiles { get; }

            public ImmutableHashSet<StaticObject> StaticObjects { get; }

            public  ImmutableHashSet<Rune> Runes { get; }
            
            public Room(AoiIndex aoiIndex, ImmutableHashSet<GroundTile> groundTiles, ImmutableHashSet<StaticObject> staticObjects, ImmutableHashSet<Rune> runes)
            {
                AoiIndex = aoiIndex;
                GroundTiles = groundTiles;
                StaticObjects = staticObjects;
                Runes = runes;
            }

        }

    }

}