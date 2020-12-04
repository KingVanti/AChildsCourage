using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game
{

    public static partial class MFloorPlanGenerating
    {

        internal static FilteredRoomPassages FilterPassagesMatching(this RoomPassageFilter filter, IEnumerable<RoomPassages> allPassages)
        {
            var filteredPassages = 
                allPassages
                .Where(p => RoomMatchesFilter(p, filter));

            return new FilteredRoomPassages(filteredPassages);
        }

        internal static bool RoomMatchesFilter(RoomPassages roomPassages, RoomPassageFilter filter) =>
            RoomTypesMatch(roomPassages, filter) &&
            LooseEndsMatch(roomPassages, filter) &&
            PassagesMatch(roomPassages, filter);

        internal static bool RoomTypesMatch(RoomPassages passages, RoomPassageFilter filter) => passages.Type == filter.RoomType;

        internal static bool LooseEndsMatch(RoomPassages roomPassages, RoomPassageFilter filter)
        {
            var looseEnds = filter.PassageFilter.FindLooseEnds(roomPassages.Passages);

            return looseEnds <= filter.MaxLooseEnds && (filter.MaxLooseEnds <= 0 || looseEnds > 0);
        }

        internal static bool PassagesMatch(RoomPassages passages, RoomPassageFilter filter) => filter.PassageFilter.Matches(passages.Passages);

    }

}