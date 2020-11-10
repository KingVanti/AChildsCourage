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

        public RoomPassages StartRoom { get; private set; }

        #endregion

        #region Constructors

        public TestRoomInfoRespository()
        {
            Reset();
        }

        #endregion

        #region Methods

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


        public FilteredRoomPassages FindFittingRoomsFor(ChunkPassageFilter filter, int remainingRoomCount)
        {
            return new FilteredRoomPassages(FindFittingPassagesFor(filter, remainingRoomCount).Select(CreateNew));
        }

        private IEnumerable<ChunkPassages> FindFittingPassagesFor(ChunkPassageFilter filter, int remainingRoomCount)
        {
            return GetAllPassages().Where(p => filter.Matches(p) && filter.FindLooseEnds(p) <= remainingRoomCount);
        }

        private IEnumerable<ChunkPassages> GetAllPassages()
        {
            return allBasePassages.SelectMany(GetVariations);
        }

        private IEnumerable<ChunkPassages> GetVariations(ChunkPassages passages)
        {
            yield return passages;
            yield return passages.XMirrored;
            yield return passages.YMirrored;
            yield return passages.Rotated;
            yield return passages.Rotated.Rotated;
            yield return passages.Rotated.Rotated.Rotated;
        }

        private RoomPassages CreateNew(ChunkPassages roomPassages)
        {
            var info = new RoomPassages(currentId, roomPassages);

            roomInfos.Add(currentId, info);

            currentId++;
            return info;
        }


        public RoomPassages GetEndRoomFor(ChunkPassageFilter filter)
        {
            return FindFittingRoomsFor(filter, 0).First();
        }

        #endregion

    }

}