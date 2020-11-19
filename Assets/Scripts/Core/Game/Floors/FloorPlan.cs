namespace AChildsCourage.Game.Floors
{

    public class FloorPlan
    {

        #region Static Properties

        internal static FloorPlan Empty { get { return new FloorPlan(new RoomPlan[0]); } }

        #endregion

        #region Properties

        public RoomPlan[] Rooms { get; }

        #endregion

        #region Constructors

        public FloorPlan()
        {
            Rooms = new RoomPlan[0];
        }

        public FloorPlan(RoomPlan[] rooms)
        {
            Rooms = rooms;
        }

        #endregion

    }

}