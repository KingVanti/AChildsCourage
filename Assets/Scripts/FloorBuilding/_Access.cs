using AChildsCourage.Game.Floors.Persistance;
using Ninject;
using Ninject.Activation;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorBuilding
    {

        private delegate RoomsForFloor RoomLoader(FloorPlan floorPlan);

        public static Floors.FloorBuilder GetFloorBuilder(IContext context)
        {
            var kernel = context.Kernel;

            IRoomRepository GetRoomRepository() =>
               kernel.Get<IRoomRepository>();

            RoomLoader GetRoomLoader(IRoomRepository roomRepository) =>
                floorPlan => new RoomsForFloor(roomRepository.LoadRoomsFor(floorPlan));

            return floorPlan =>
            {
                var roomRepository = GetRoomRepository();
                var roomLoader = GetRoomLoader(roomRepository);

                return Build(floorPlan, roomLoader);
            };
        }

    }

}