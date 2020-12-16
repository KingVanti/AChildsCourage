using System.Collections.Immutable;

namespace AChildsCourage.Game.Floors
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