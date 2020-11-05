using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors.Generation
{

    internal class GenerationSession : IGenerationSession
    {

        #region Constants

        private const int MaxRoomCount = 15;

        #endregion

        #region Fields

        private readonly IRNG rng;
        private readonly IChunkGrid chunkGrid;
        private readonly IRoomInfoRepository roomInfoRepository;
        private readonly List<int> usedRoomIds = new List<int>();

        #endregion

        #region Properties

        private bool CanPlaceMoreRooms { get { return chunkGrid.RoomCount < NormalRoomCount; } }

        private int NormalRoomCount { get { return MaxRoomCount - DeadEndCount; } }

        private int DeadEndCount { get { return chunkGrid.FindDeadEndChunks().Length; } }

        #endregion

        #region Constructors

        internal GenerationSession(IRNG rng, IChunkGrid chunkGrid, IRoomInfoRepository roomInfoRepository)
        {
            this.rng = rng;
            this.chunkGrid = chunkGrid;
            this.roomInfoRepository = roomInfoRepository;
        }

        #endregion

        #region Methods

        public FloorPlan Generate()
        {
            PlaceStartRoom();
            PlaceNormalRooms();
            PlaceEndRoom();
            PlaceDeadEnds();

            return chunkGrid.BuildPlan();
        }

        internal void PlaceStartRoom()
        {
            Place(roomInfoRepository.StartRoom, new ChunkPosition(0, 0));
        }

        internal void PlaceNormalRooms()
        {
            while (CanPlaceMoreRooms)
            {
                var chunkPosition = chunkGrid.FindNextBuildChunk(rng);
                var roomInfo = GetRoomFor(chunkPosition);

                Place(roomInfo, chunkPosition);
            }
        }

        internal void PlaceDeadEnds()
        {
            var positions = chunkGrid.FindDeadEndChunks();

            foreach (var position in positions)
            {
                var roomInfo = GetRoomFor(position);

                Place(roomInfo, position);
            }
        }

        internal void PlaceEndRoom()
        {
            ChunkPosition endroomChunk = chunkGrid.FindDeadEndChunks().GetRandom(rng);

            Place(roomInfoRepository.EndRoom, endroomChunk);
        }


        private void Place(RoomInfo room, ChunkPosition position)
        {
            usedRoomIds.Add(room.RoomId);
            chunkGrid.Place(room, position);
        }


        private RoomInfo GetRoomFor(ChunkPosition chunkPosition)
        {
            var passages = chunkGrid.GetPassagesTo(chunkPosition);
            var potentialRooms = roomInfoRepository.FindFittingRoomsFor(passages);

            return ChooseRoomFrom(potentialRooms);
        }

        private RoomInfo ChooseRoomFrom(IEnumerable<RoomInfo> potentialRooms)
        {
            return
                potentialRooms
                .Where(IsValid)
                .GetRandom(rng);
        }

        private bool IsValid(RoomInfo room)
        {
            return !IsUsed(room);
        }

        private bool IsUsed(RoomInfo room)
        {
            return usedRoomIds.Contains(room.RoomId);
        }

        #endregion

    }

}