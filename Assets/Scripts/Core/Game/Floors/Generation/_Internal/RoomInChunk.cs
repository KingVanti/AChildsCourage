namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct RoomInChunk
    {

        #region Properties

        public int RoomId { get; }

        public ChunkPosition Position { get; }

        #endregion

        #region Constructors

        public RoomInChunk(int roomId, ChunkPosition position)
        {
            RoomId = roomId;
            Position = position;
        }

        #endregion

    }

}