using System;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MRoomPassageFiltering
        {

            public static FilteredRoomPassages FilterPassagesMatching(RoomPassageFilter filter, IEnumerable<RoomPassages> allPassages)
            {
                var filteredPassages = allPassages
                                       .Where(p => RoomMatchesFilter(p, filter))
                                       .ToArray();

                if (filteredPassages.Length > 0)
                    return new FilteredRoomPassages(filteredPassages);
                throw new Exception($"No passages for the filter: {filter}!");
            }

            private static bool RoomMatchesFilter(RoomPassages roomPassages, RoomPassageFilter filter) =>
                RoomTypesMatch(roomPassages, filter) &&
                PassagesMatch(roomPassages, filter);

            private static bool RoomTypesMatch(RoomPassages passages, RoomPassageFilter filter) => passages.Type == filter.RoomType;

            private static bool PassagesMatch(RoomPassages passages, RoomPassageFilter filter) => filter.Passages.Equals(passages.Passages);

        }

    }

}