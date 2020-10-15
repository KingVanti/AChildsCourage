using AChildsCourage.Game.FloorGeneration.Persistance;

namespace AChildsCourage.Game.FloorGeneration
{

    public class Room
    {

        #region Properties

        public RoomShape Shape { get; }

        public RoomEntities Items { get; }

        #endregion

        #region Constructors

        public Room(RoomShape shape, RoomEntities items)
        {
            Shape = shape;
            Items = items;
        }

        #endregion

    }

}