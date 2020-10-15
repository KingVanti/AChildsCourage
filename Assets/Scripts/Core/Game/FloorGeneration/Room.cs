using AChildsCourage.Game.FloorGeneration.Persistance;

namespace AChildsCourage.Game.FloorGeneration
{

    public class Room
    {

        #region Properties

        public RoomShape Shape { get; }

        public RoomItems Items { get; }

        #endregion

        #region Constructors

        public Room(RoomShape shape, RoomItems items)
        {
            Shape = shape;
            Items = items;
        }

        #endregion

    }

}