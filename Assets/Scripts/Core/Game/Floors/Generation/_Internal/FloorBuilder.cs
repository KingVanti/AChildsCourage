namespace AChildsCourage.Game.Floors.Generation
{

    internal class FloorBuilder : IFloorBuilder
    {

        #region Fields

        private readonly IRoomBuilder roomBuilder;

        #endregion

        #region Constructors

        public FloorBuilder(IRoomBuilder roomBuilder)
        {
            this.roomBuilder = roomBuilder;
        }

        #endregion

        #region Methods

        public void Build(FloorPlan floorPlan)
        {
            BuildRooms(floorPlan.RoomPositions);
        }

        private void BuildRooms(RoomAtPosition[] roomPositions)
        {
            foreach (var roomPosition in roomPositions)
                roomBuilder.Build(roomPosition);
        }

        #endregion

    }

}