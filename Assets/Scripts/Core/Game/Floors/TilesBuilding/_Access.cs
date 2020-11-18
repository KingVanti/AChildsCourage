using AChildsCourage.Game.Floors.Persistance;
using Ninject;
using Ninject.Activation;
using Ninject.Parameters;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        public static BuildRoomTiles GetFloorBuilder(IContext context)
        {
            var kernel = context.Kernel;

            IRoomRepository GetRoomRepository() =>
               kernel.Get<IRoomRepository>();

            IRNG GetRNG() =>
                kernel.Get<IRNG>(new ConstructorArgument("seed", 0));

            LoadRoomsFor LoadRoomsFrom(IRoomRepository roomRepository) =>
                floorPlan => new RoomsForFloor(roomRepository.LoadRoomsFor(floorPlan));

            return floorPlan =>
            {
                var roomRepository = GetRoomRepository();
                var loadRoomsFor = LoadRoomsFrom(roomRepository);

                var rng = GetRNG();

                return Build(floorPlan, loadRoomsFor, rng);
            };
        }

    }

}