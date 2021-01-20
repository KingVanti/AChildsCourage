using System.Collections.Generic;
using System.Collections.Immutable;

namespace AChildsCourage.Game.Floors
{

    internal readonly struct RoomContent
    {

        internal static RoomContent Create(IEnumerable<FloorObject> objects) =>
            new RoomContent(objects.ToImmutableHashSet());


        internal ImmutableHashSet<FloorObject> Objects { get; }


        private RoomContent(ImmutableHashSet<FloorObject> objects) =>
            Objects = objects;

    }

}