using System;

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

        #endregion

        #region Properties

        private bool CanPlaceMoreRooms { get { throw new NotImplementedException(); } }

        #endregion

        #region Constructors

        internal GenerationSession(IRNG rng, IChunkGrid chunkGrid)
        {
            this.rng = rng;
            this.chunkGrid = chunkGrid;
        }

        #endregion

        #region Methods

        public FloorPlan Generate()
        {
            PlaceStartRoom();
            PlaceNormalRooms();
            PlaceDeadEnds();
            PlaceEndRoom();

            return chunkGrid.BuildPlan();
        }

        private void PlaceStartRoom()
        {
            chunkGrid.Place(GetStartRoomInfo(), new ChunkPosition(0, 0));
        }

        private RoomInfo GetStartRoomInfo()
        {
            throw new NotImplementedException();
        }


        private void PlaceNormalRooms()
        {
            while (CanPlaceMoreRooms)
            {
                var chunkPosition = chunkGrid.FindNextBuildChunk(rng);
                var roomInfo = GetRoomFor(chunkPosition);

                chunkGrid.Place(roomInfo, chunkPosition);
            }
        }

        private RoomInfo GetRoomFor(ChunkPosition chunkPosition)
        {
            throw new NotImplementedException();
        }


        private void PlaceDeadEnds()
        {
            throw new NotImplementedException();
        }


        private void PlaceEndRoom()
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}