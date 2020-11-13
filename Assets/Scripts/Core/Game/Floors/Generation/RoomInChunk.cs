namespace AChildsCourage.Game.Floors.Generation
{
    public class RoomInChunk
    {

        #region Properties

        public ChunkPosition Position { get; }

        public Room Room { get; }

        #endregion

        #region Constructors

        public RoomInChunk(ChunkPosition position, Room room)
        {
            Position = position;
            Room = room;
        }

        #endregion

    }
}