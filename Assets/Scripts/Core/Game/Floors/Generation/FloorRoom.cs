namespace AChildsCourage.Game.Floors.Generation
{
    public class FloorRoom
    {

        #region Properties

        public ChunkPosition Position { get; }

        public Room Room { get; }

        #endregion

        #region Constructors

        public FloorRoom(ChunkPosition position, Room room)
        {
            Position = position;
            Room = room;
        }

        #endregion

    }
}