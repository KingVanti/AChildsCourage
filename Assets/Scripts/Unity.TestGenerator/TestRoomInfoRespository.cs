using System.Collections.Generic;

namespace AChildsCourage.Game.Floors.Generation.Editor
{

    public class TestRoomInfoRespository : IRoomInfoRepository
    {

        #region Fields

        private int currentId = 0;
        private readonly Dictionary<int, RoomInfo> roomInfos = new Dictionary<int, RoomInfo>();
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

        public RoomInfo StartRoom { get; private set; }

        public RoomInfo EndRoom { get; private set; }

        #endregion

        #region Constructors

        public TestRoomInfoRespository()
        {
            Reset();
        }

        #endregion

        #region Methods

        public RoomInfo GetById(int roomId)
        {
            return roomInfos[roomId];
        }


        public void Reset()
        {
            currentId = 0;
            roomInfos.Clear();

            StartRoom = CreateNew(ChunkPassages.All);
            EndRoom = CreateNew(new ChunkPassages(true, false, false, false));
        }


        public IEnumerable<RoomInfo> FindFittingRoomsFor(ChunkPassageFilter filter, int remainingRoomCount)
        {
            foreach (var roomPassages in FindFittingPassagesFor(filter, remainingRoomCount))
                yield return CreateNew(roomPassages);
        }

        private IEnumerable<ChunkPassages> FindFittingPassagesFor(ChunkPassageFilter filter, int remainingRoomCount)
        {
            foreach (var passages in GetAllPassages())
                if (filter.Matches(passages) && filter.FindLooseEnds(passages) <= remainingRoomCount)
                    yield return passages;
        }

        private IEnumerable<ChunkPassages> GetAllPassages()
        {
            foreach (var basePassages in allBasePassages)
                foreach (var passage in GetVariations(basePassages))
                    yield return passage;
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

        private RoomInfo CreateNew(ChunkPassages roomPassages)
        {
            var info = new RoomInfo(currentId, roomPassages);

            roomInfos.Add(currentId, info);

            currentId++;
            return info;
        }

        #endregion

    }

}