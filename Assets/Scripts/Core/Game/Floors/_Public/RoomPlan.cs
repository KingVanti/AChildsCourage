namespace AChildsCourage.Game.Floors
{

    public readonly struct RoomPlan
    {

        #region Properties

        public int RoomId { get; }

        public ChunkPosition Position { get; }

        #endregion

        #region Constructors

        public RoomPlan(int roomId, ChunkPosition position)
        {
            RoomId = roomId;
            Position = position;
        }

        #endregion

    }

}