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

        private bool CanPlaceMoreNormalRooms { get { return UsedChunksCount <= MaxRoomCount - 1; } }

        private int UsedChunksCount { get { return chunkGrid.RoomCount + chunkGrid.ReservedChunkCount; } }

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

            return chunkGrid.BuildPlan();
        }

        internal void PlaceStartRoom()
        {
            Place(roomInfoRepository.StartRoom, new ChunkPosition(0, 0));
        }

        internal void PlaceNormalRooms()
        {
            while (CanPlaceMoreNormalRooms)
            {
                var chunkPosition = chunkGrid.FindNextBuildChunk(rng);
                var roomInfo = GetRoomFor(chunkPosition);

                Place(roomInfo, chunkPosition);
            }
        }

        internal void PlaceEndRoom()
        {
            ChunkPosition endroomChunk = chunkGrid.FindNextBuildChunk(rng);

            Place(roomInfoRepository.EndRoom, endroomChunk);
        }


        private void Place(RoomInfo room, ChunkPosition position)
        {
            usedRoomIds.Add(room.RoomId);
            chunkGrid.Place(room, position);
        }


        private RoomInfo GetRoomFor(ChunkPosition chunkPosition)
        {
            var filter = chunkGrid.GetFilterFor(chunkPosition);
            var potentialRooms = roomInfoRepository.FindFittingRoomsFor(filter);

            return ChooseRoomFrom(potentialRooms);
        }

        private RoomInfo ChooseRoomFrom(IEnumerable<RoomInfo> potentialRooms)
        {
            var validRooms =
                potentialRooms
                .Where(IsValid);

            if (validRooms.Count() > 0)
                return validRooms.GetRandom(rng);
            else
                throw new System.Exception("No valid rooms found!");
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