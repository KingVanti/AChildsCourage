using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public class FilteredRoomPassages : IEnumerable<RoomPassages>
        {

            private readonly ImmutableHashSet<RoomPassages> passages;


            public FilteredRoomPassages(IEnumerable<RoomPassages> passages) => this.passages = passages.ToImmutableHashSet();


            public IEnumerator<RoomPassages> GetEnumerator() => passages.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => passages.GetEnumerator();

        }

    }

}