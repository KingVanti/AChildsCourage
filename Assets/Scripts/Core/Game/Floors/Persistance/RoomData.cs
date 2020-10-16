using AChildsCourage.Game.Floors.Persistance;

namespace AChildsCourage.Game.Floors
{

    public class RoomData
    {

        #region Properties

        public RoomShape Shape { get; }

        public RoomEntities Items { get; }

        #endregion

        #region Constructors

        public RoomData(RoomShape shape, RoomEntities items)
        {
            Shape = shape;
            Items = items;
        }

        #endregion

    }

}