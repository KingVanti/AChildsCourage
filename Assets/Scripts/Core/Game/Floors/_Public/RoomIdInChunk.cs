namespace AChildsCourage.Game.Floors
{

    public readonly struct RoomIdInChunk
    {

        #region Properties

        public int RoomId { get; }

        public ChunkPosition Position { get; }

        #endregion

        #region Constructors

        internal RoomIdInChunk(int roomId, ChunkPosition position)
        {
            RoomId = roomId;
            Position = position;
        }

        #endregion

    }

}