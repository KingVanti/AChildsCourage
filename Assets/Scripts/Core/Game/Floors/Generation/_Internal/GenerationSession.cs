using Castle.Components.DictionaryAdapter.Xml;
using System;

namespace AChildsCourage.Game.Floors.Generation {

    internal class GenerationSession : IGenerationSession {

        #region Constants

        private const int MaxRoomCount = 15;

        #endregion

        #region Fields

        private readonly IRNG rng;
        private readonly IChunkGrid chunkGrid;
        private readonly IRoomInfoRepository roomInfoRepository;


        #endregion

        #region Properties

        private bool CanPlaceMoreRooms {
            get {

                return (chunkGrid.RoomCount < (MaxRoomCount - chunkGrid.FindDeadEndChunks().Length));

            }
        }

        #endregion

        #region Constructors

        internal GenerationSession(IRNG rng, IChunkGrid chunkGrid, IRoomInfoRepository roomInfoRepository) {
            this.rng = rng;
            this.chunkGrid = chunkGrid;
            this.roomInfoRepository = roomInfoRepository;
        }

        #endregion

        #region Methods

        public FloorPlan Generate() {
            PlaceStartRoom();
            PlaceNormalRooms();
            PlaceEndRoom();
            PlaceDeadEnds();

            return chunkGrid.BuildPlan();
        }

        private void PlaceStartRoom() {
            chunkGrid.Place(roomInfoRepository.StartRoom, new ChunkPosition(0, 0));
        }


        private void PlaceNormalRooms() {
            while (CanPlaceMoreRooms) {
                var chunkPosition = chunkGrid.FindNextBuildChunk(rng);
                var roomInfo = GetRoomFor(chunkPosition);

                chunkGrid.Place(roomInfo, chunkPosition);
            }
        }

        private RoomInfo GetRoomFor(ChunkPosition chunkPosition) {
            var passages = chunkGrid.GetPassagesTo(chunkPosition);
            return roomInfoRepository.TryFindRoomFor(passages);
        }

        private void PlaceDeadEnds() {

            ChunkPosition[] cps = chunkGrid.FindDeadEndChunks();

            foreach (ChunkPosition cp in cps) {
                var roomInfo = GetRoomFor(cp);
                chunkGrid.Place(roomInfo, cp);
            }


        }

        private void PlaceEndRoom() {
            var chunkPosition = chunkGrid.FindNextBuildChunk(rng);

            if (chunkGrid.GetPassagesTo(chunkPosition).Passages.Length == 1) {
                chunkGrid.Place(roomInfoRepository.EndRoom, chunkPosition);
            }
        }

        #endregion

    }

}