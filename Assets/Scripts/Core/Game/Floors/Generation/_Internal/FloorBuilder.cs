namespace AChildsCourage.Game.Floors.Generation
{

    [Singleton]
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
            BuildRooms(floorPlan.Rooms);
        }

        private void BuildRooms(RoomInChunk[] rooms)
        {
            foreach (var room in rooms)
                roomBuilder.Build(room);
        }

        #endregion

    }

}