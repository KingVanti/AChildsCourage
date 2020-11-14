namespace AChildsCourage.Game.Floors.Generation
{
    internal interface IChunkGrid
    {

        #region Properties

        int RoomCount { get; }

        int ReservedChunkCount { get; }

        #endregion

        #region Methods

        FloorPlan BuildPlan();

        ChunkPosition FindNextBuildChunk(IRNG rng);

        ChunkPassageFilter GetFilterFor(ChunkPosition position);

        void Place(RoomPassages room, ChunkPosition position);

        #endregion

    }
}