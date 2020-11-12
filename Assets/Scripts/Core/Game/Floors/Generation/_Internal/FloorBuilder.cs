using AChildsCourage.Game.Floors.Persistance;

namespace AChildsCourage.Game.Floors.Generation
{

    [Singleton]
    internal class FloorBuilder : IFloorBuilder
    {

        #region Fields

        private readonly IRoomRepository roomRepository;
        private readonly IRoomBuilder roomBuilder;

        #endregion

        #region Constructors

        public FloorBuilder(IRoomRepository roomRepository, IRoomBuilder roomBuilder)
        {
            this.roomRepository = roomRepository;
            this.roomBuilder = roomBuilder;
        }

        #endregion

        #region Methods

        public void Build(FloorPlan floorPlan)
        {
            var floorRooms = roomRepository.LoadRoomsFor(floorPlan);

            BuildRooms(floorRooms);
        }

        private void BuildRooms(FloorRooms rooms)
        {
            foreach (var room in rooms)
                roomBuilder.Build(room.Tiles, room.Position);
        }

        #endregion

    }

}