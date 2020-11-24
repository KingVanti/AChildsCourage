using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using static AChildsCourage.F;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.NightLoading
{

    internal static class NightLoading
    {

        internal static NightLoader Make(IRoomPassagesRepository roomPassagesRepository, RoomDataLoader roomDataLoader, IFloorRecreator floorRecreator)
        {
            return nightData =>
            {
                var roomData = roomDataLoader();

                RNGInitializer rngInitializer = SeedBasedRNG;
                var floorPlanGenerator = FloorPlanGenerating.Make(roomPassagesRepository, rngInitializer);

                var floorGenerator = FloorGenerating.Make(roomData);

                var nightRecreator = NightRecreating.Make(floorRecreator);

                Take(nightData.Seed)
                .Map(floorPlanGenerator.Invoke)
                .Map(floorGenerator.Invoke)
                .Do(nightRecreator.Invoke);
            };
        }

    }

}
