namespace AChildsCourage.Game.Floors.Generation
{
    public interface IChunkGrid
    {

        #region Properties

        int RoomCount { get; }

        int ReservedChunkCount { get; }

        #endregion

        #region Methods

        FloorPlan BuildPlan();

        ChunkPosition FindNextBuildChunk(IRNG rng);

        ChunkPassageFilter GetFilterFor(ChunkPosition position);

        void Place(RoomInfo room, ChunkPosition position);

        #endregion

    }
}