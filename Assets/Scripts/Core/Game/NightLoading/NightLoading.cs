using AChildsCourage.Game.Floors;
using static AChildsCourage.F;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.NightLoading
{

    internal static class NightLoading
    {

        internal static NightLoader Make(IRoomPassagesRepository roomPassagesRepository, IRoomRepository roomRepository, IFloorRecreator floorRecreator)
        {
            return nightData =>
            {
                RNGInitializer rngInitializer = SeedBasedRNG;
                var floorPlanGenerator = FloorPlanGenerating.Make(roomPassagesRepository, rngInitializer);

                RoomLoader roomLoader = floorPlan => roomRepository.LoadRoomsFor(floorPlan);
                var floorGenerator = FloorGenerating.Make(roomLoader);

                var nightRecreator = NightRecreating.Make(floorRecreator);

                Take(nightData.Seed)
                .Map(floorPlanGenerator.Invoke)
                .Map(floorGenerator.Invoke)
                .Do(nightRecreator.Invoke);
            };
        }

    }

}
