using System;
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
        private readonly IRoomPassagesRepository roomInfoRepository;
        private readonly List<int> usedRoomIds = new List<int>();

        #endregion

        #region Properties

        private int RemainingRoomCount { get { return MaxRoomCount - UsedChunksCount; } }

        private int UsedChunksCount { get { return chunkGrid.RoomCount + chunkGrid.ReservedChunkCount; } }

        #endregion

        #region Constructors

        public GenerationSession(IRNG rng, IChunkGrid chunkGrid, IRoomPassagesRepository roomInfoRepository)
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
            var startRoom = ChooseStartRoom();

            Place(startRoom, new ChunkPosition(0, 0));
        }

        private RoomPassages ChooseStartRoom()
        {
            var startRooms = roomInfoRepository.GetStartRooms();

            return startRooms.GetRandom(rng);
        }

        internal void PlaceNormalRooms()
        {
            for (var i = 0; i < MaxRoomCount - 2; i++)
            {
                var chunkPosition = chunkGrid.FindNextBuildChunk(rng);
                var roomInfo = GetRoomFor(chunkPosition);

                Place(roomInfo, chunkPosition);
            }
        }

        internal void PlaceEndRoom()
        {
            ChunkPosition endroomChunk = chunkGrid.FindNextBuildChunk(rng);
            var endRoom = ChooseEndRoom(endroomChunk);

            Place(endRoom, endroomChunk);
        }

        private RoomPassages ChooseEndRoom(ChunkPosition position)
        {
            var filter = chunkGrid.GetFilterFor(position);
            var endRooms = roomInfoRepository.GetEndRooms(filter);

            return endRooms.GetRandom(rng);
        }


        private void Place(RoomPassages room, ChunkPosition position)
        {
            usedRoomIds.Add(room.RoomId);
            chunkGrid.Place(room, position);
        }


        private RoomPassages GetRoomFor(ChunkPosition chunkPosition)
        {
            var filter = chunkGrid.GetFilterFor(chunkPosition);
            var filteredRooms = roomInfoRepository.GetNormalRooms(filter, RemainingRoomCount);

            return ChooseRoomFrom(filteredRooms);
        }

        private RoomPassages ChooseRoomFrom(FilteredRoomPassages rooms)
        {
            var validRooms =
                rooms
                .Where(IsValid);

            if (validRooms.Count() > 0)
                return validRooms.GetWeightedRandom(CalculateRoomWeight, rng);
            else
                throw new System.Exception("No valid rooms found!");
        }

        private bool IsValid(RoomPassages room)
        {
            return !IsUsed(room);
        }

        private bool IsUsed(RoomPassages room)
        {
            return usedRoomIds.Contains(room.RoomId);
        }

        private float CalculateRoomWeight(RoomPassages room)
        {
            return CalculatePassageWeight(room);
        }

        private float CalculatePassageWeight(RoomPassages room)
        {
            return (float)Math.Pow(room.Passages.Count, 2);
        }

        #endregion

    }

}