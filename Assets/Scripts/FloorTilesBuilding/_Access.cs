using AChildsCourage.Game.Floors.Persistance;
using Ninject;
using Ninject.Activation;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private delegate RoomsForFloor LoadRoomsFor(FloorPlan floorPlan);

        public static BuildRoomTiles GetFloorBuilder(IContext context)
        {
            var kernel = context.Kernel;

            IRoomRepository GetRoomRepository() =>
               kernel.Get<IRoomRepository>();

            LoadRoomsFor LoadRoomsFrom(IRoomRepository roomRepository) =>
                floorPlan => new RoomsForFloor(roomRepository.LoadRoomsFor(floorPlan));

            return floorPlan =>
            {
                var roomRepository = GetRoomRepository();
                var loadRoomsFor = LoadRoomsFrom(roomRepository);

                return Build(floorPlan, loadRoomsFor);
            };
        }

    }

}