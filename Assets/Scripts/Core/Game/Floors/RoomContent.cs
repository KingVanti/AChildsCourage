using System.Collections.Generic;
using System.Collections.Immutable;

namespace AChildsCourage.Game.Floors
{

    public readonly struct RoomContent
    {

        public static RoomContent Create(IEnumerable<FloorObject> objects) =>
            new RoomContent(objects.ToImmutableHashSet());

        
        public ImmutableHashSet<FloorObject> Objects { get; }


        private RoomContent(ImmutableHashSet<FloorObject> objects) =>
            Objects = objects;

    }

}