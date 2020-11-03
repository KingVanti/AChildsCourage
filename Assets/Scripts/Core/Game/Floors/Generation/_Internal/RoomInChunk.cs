namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct RoomInChunk
    {

        #region Properties

        public RoomInfo Room { get; }

        public ChunkPosition Position { get; }

        #endregion

        #region Constructors

        public RoomInChunk(RoomInfo room, ChunkPosition position)
        {
            Room = room;
            Position = position;
        }

        #endregion

    }

}