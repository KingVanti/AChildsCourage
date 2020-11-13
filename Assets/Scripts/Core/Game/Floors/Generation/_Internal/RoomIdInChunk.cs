namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct RoomIdInChunk
    {

        #region Properties

        public int RoomId { get; }

        public ChunkPosition Position { get; }

        #endregion

        #region Constructors

        public RoomIdInChunk(int roomId, ChunkPosition position)
        {
            RoomId = roomId;
            Position = position;
        }

        #endregion

    }

}