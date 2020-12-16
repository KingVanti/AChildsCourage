using System.Collections.Immutable;
using AChildsCourage.Game.Shade.Navigation;

namespace AChildsCourage.Game.Floors
{

    public static class MRoom
    {

        public readonly struct Room
        {
            
            public ImmutableHashSet<GroundTile> GroundTiles { get; }

            public ImmutableHashSet<StaticObject> StaticObjects { get; }

            public Room(ImmutableHashSet<GroundTile> groundTiles, ImmutableHashSet<StaticObject> staticObjects)
            {
                GroundTiles = groundTiles;
                StaticObjects = staticObjects;
            }

        }

    }

}