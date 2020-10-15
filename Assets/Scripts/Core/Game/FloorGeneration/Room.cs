namespace AChildsCourage.Game.FloorGeneration
{

    public class Room
    {

        #region Properties

        public RoomShape Shape { get; }

        #endregion

        #region Constructors

        public Room(RoomShape shape)
        {
            Shape = shape;
        }

        #endregion

    }

}