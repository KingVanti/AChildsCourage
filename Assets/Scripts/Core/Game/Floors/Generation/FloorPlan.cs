namespace AChildsCourage.Game.Floors.Generation
{

    public class FloorPlan
    {

        #region Properties

        public RoomAtPosition[] RoomPositions { get; }

        #endregion

        #region Constructors

        internal FloorPlan()
        {
            RoomPositions = new RoomAtPosition[0];
        }

        internal FloorPlan(RoomAtPosition[] roomPositions)
        {
            RoomPositions = roomPositions;
        }

        #endregion

    }

}