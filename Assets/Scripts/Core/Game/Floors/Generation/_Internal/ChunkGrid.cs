using System;

namespace AChildsCourage.Game.Floors.Generation
{

    internal class ChunkGrid : IChunkGrid
    {

        #region Properties

        public int RoomCount { get { throw new NotImplementedException(); } }

        #endregion

        #region Methods

        public ChunkPosition FindNextBuildChunk(IRNG rng)
        {
            throw new NotImplementedException();
        }

        internal float GetChunkWeight(ChunkPosition position)
        {
            throw new NotImplementedException();
        }

        internal bool CanPlaceAt(ChunkPosition position)
        {
            throw new NotImplementedException();
        }


        public void Place(RoomInfo room, ChunkPosition position)
        {
            throw new NotImplementedException();
        }


        public FloorPlan BuildPlan()
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}