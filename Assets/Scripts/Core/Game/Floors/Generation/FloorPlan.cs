namespace AChildsCourage.Game.Floors.Generation
{

    public class FloorPlan
    {

        #region Static Properties

        public static FloorPlan Empty { get { return new FloorPlan(new RoomIdInChunk[0]); } }

        #endregion

        #region Properties

        public RoomIdInChunk[] Rooms { get; }

        #endregion

        #region Constructors

        internal FloorPlan()
        {
            Rooms = new RoomIdInChunk[0];
        }

        internal FloorPlan(RoomIdInChunk[] rooms)
        {
            Rooms = rooms;
        }

        #endregion

    }

}