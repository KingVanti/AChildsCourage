using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    internal class ResourceRoomPassageRepository : IRoomPassagesRepository
    {

        #region Constants

        private const string RoomResourcePath = "Rooms/";

        #endregion

        #region Fields

        private readonly RoomPassages[] startRooms;
        private readonly RoomPassages[] normalRooms;
        private readonly RoomPassages[] endRooms;

        #endregion

        #region Constructors

        public ResourceRoomPassageRepository()
        {
            var allRooms = GetAllPassages();

            startRooms = allRooms.Where(r => r.Type == RoomType.Start).ToArray();
            normalRooms = allRooms.Where(r => r.Type == RoomType.Normal).ToArray();
            endRooms = allRooms.Where(r => r.Type == RoomType.End).ToArray();
        }

        #endregion

        #region Methods

        public FilteredRoomPassages GetRooms(RoomPassageFilter filter)
        {
            switch (filter.RoomType)
            {
                case RoomType.Start:
                    return GetStartRooms();
                case RoomType.Normal:
                    return GetNormalRooms(filter.PassageFilter, filter.MaxLooseEnds);
                case RoomType.End:
                    return GetEndRooms(filter.PassageFilter);
                default:
                    throw new System.Exception("Invalid room type!");
            }
        }

        public FilteredRoomPassages GetStartRooms()
        {
            return new FilteredRoomPassages(startRooms);
        }


        public FilteredRoomPassages GetNormalRooms(ChunkPassageFilter filter, int maxLooseEnds)
        {
            var filteredRooms = FilterRoomsFor(filter, maxLooseEnds);

            return new FilteredRoomPassages(filteredRooms);
        }

        private IEnumerable<RoomPassages> FilterRoomsFor(ChunkPassageFilter filter, int maxLooseEnds)
        {
            bool MatchesFilter(RoomPassages room) => RoomMatchesFilter(room, filter, maxLooseEnds);

            return
                normalRooms
                .Where(MatchesFilter);
        }


        public FilteredRoomPassages GetEndRooms(ChunkPassageFilter filter)
        {
            bool MatchesFilter(RoomPassages room) => RoomMatchesFilter(room, filter, 0);

            var filteredRooms = endRooms.Where(MatchesFilter).ToArray();

            return new FilteredRoomPassages(endRooms);
        }


        private IEnumerable<RoomPassages> GetAllPassages()
        {
            var assets = LoadAssets();

            return
                assets
                .SelectMany(a => a.GetPassages());
        }

        private IEnumerable<RoomAsset> LoadAssets()
        {
            return Resources.LoadAll<RoomAsset>(RoomResourcePath).OrderBy(a => a.Id);
        }

        private bool RoomMatchesFilter(RoomPassages roomPassages, ChunkPassageFilter filter, int maxLooseEnds)
        {
            var looseEnds = filter.FindLooseEnds(roomPassages.Passages);

            var passagesMatch = filter.Matches(roomPassages.Passages);
            var looseEndsMatch = looseEnds <= maxLooseEnds && (maxLooseEnds > 0 ? looseEnds > 0 : true);

            return passagesMatch && looseEndsMatch;
        }

        #endregion

    }

}