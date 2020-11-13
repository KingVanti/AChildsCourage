namespace AChildsCourage.Game.Floors.Generation
{
    public class RoomAtPosition
    {

        #region Properties

        public ChunkPosition Position { get; }

        public Room Room { get; }

        #endregion

        #region Constructors

        public RoomAtPosition(ChunkPosition position, Room room)
        {
            Position = position;
            Room = room;
        }

        #endregion

    }
}