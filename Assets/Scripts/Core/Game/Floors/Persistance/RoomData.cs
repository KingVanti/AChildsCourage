namespace AChildsCourage.Game.Floors.Persistance
{

    public class RoomData
    {

        #region Properties

        public RoomShape Shape { get; }

        public RoomEntities Entities { get; }

        #endregion

        #region Constructors

        public RoomData(RoomShape shape, RoomEntities entities)
        {
            Shape = shape;
            Entities = entities;
        }

        #endregion

    }

}