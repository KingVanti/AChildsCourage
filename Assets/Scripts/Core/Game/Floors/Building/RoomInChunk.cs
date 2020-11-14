namespace AChildsCourage.Game.Floors.Building
{
    public class RoomInChunk
    {

        #region Properties

        internal ChunkPosition Position { get; }

        internal Room Room { get; }

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