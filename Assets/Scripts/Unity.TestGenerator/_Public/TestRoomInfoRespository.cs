using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors.Generation.Editor
{

    public class TestRoomInfoRespository : IRoomPassagesRepository
    {

        #region Fields

        private int currentId = 0;
        private readonly Dictionary<int, RoomPassages> roomInfos = new Dictionary<int, RoomPassages>();
        private readonly ChunkPassages[] allBasePassages = new[]
        {
            new ChunkPassages(true, false, false, false),
            new ChunkPassages(true, true, false, false),
            new ChunkPassages(true, false, true, false),
            new ChunkPassages(true, true, true, false),
            new ChunkPassages(true, true, true, true)
        };

        #endregion

        #region Properties

        private RoomPassages StartRoom { get; set; }

        #endregion

        #region Constructors

        public TestRoomInfoRespository()
        {
            Reset();
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

        private FilteredRoomPassages GetStartRooms()
        {
            return new FilteredRoomPassages(new[] { StartRoom });
        }


        public RoomPassages GetById(int roomId)
        {
            return roomInfos[roomId];
        }


        public void Reset()
        {
            currentId = 0;
            roomInfos.Clear();

            StartRoom = CreateNew(ChunkPassages.All);
        }

        private FilteredRoomPassages GetNormalRooms(ChunkPassageFilter filter, int maxLooseEnds)
        {
            return new FilteredRoomPassages(FindFittingPassagesFor(filter, maxLooseEnds).Select(CreateNew));
        }

        private IEnumerable<ChunkPassages> FindFittingPassagesFor(ChunkPassageFilter filter, int maxLooseEnds)
        {
            bool MatchesFilter(ChunkPassages passages) => PassagesMatchesFilter(passages, filter, maxLooseEnds);

            return GetAllPassages().Where(MatchesFilter);
        }

        private bool PassagesMatchesFilter(ChunkPassages passages, ChunkPassageFilter filter, int maxLooseEnds)
        {
            var looseEnds = filter.FindLooseEnds(passages);

            var passagesMatch = filter.Matches(passages);
            var looseEndsMatch = looseEnds <= maxLooseEnds && (maxLooseEnds > 0 ? looseEnds > 0 : true);

            return passagesMatch && looseEndsMatch;
        }

        private IEnumerable<ChunkPassages> GetAllPassages()
        {
            return allBasePassages.SelectMany(GetVariations);
        }

        private IEnumerable<ChunkPassages> GetVariations(ChunkPassages passages)
        {
            yield return passages;
            yield return passages.XMirrored;
            yield return passages.Rotated;
            yield return passages.Rotated.XMirrored;
            yield return passages.Rotated.Rotated;
            yield return passages.Rotated.Rotated.XMirrored;
            yield return passages.Rotated.Rotated.Rotated;
            yield return passages.Rotated.Rotated.Rotated.XMirrored;
        }

        private RoomPassages CreateNew(ChunkPassages roomPassages)
        {
            var info = new RoomPassages(currentId, roomPassages);

            roomInfos.Add(currentId, info);

            currentId++;
            return info;
        }

        private FilteredRoomPassages GetEndRooms(ChunkPassageFilter filter)
        {
            return GetNormalRooms(filter, 0);
        }

        #endregion

    }

}