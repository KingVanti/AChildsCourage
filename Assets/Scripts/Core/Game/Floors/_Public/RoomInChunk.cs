namespace AChildsCourage.Game.Floors
{
    public class RoomInChunk
    {

        #region Properties

        public Room Room { get; }

        public RoomTransform Transform { get; }

        #endregion

        #region Constructors

        public RoomInChunk(Room room, RoomTransform transform)
        {
            Room = room;
            Transform = transform;
        }

        #endregion

    }
}