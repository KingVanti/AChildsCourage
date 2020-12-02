using AChildsCourage.Game.Floors.RoomPersistance;
using static AChildsCourage.F;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game
{

    public static class MNightPreparation
    {

        public delegate void PrepareNight(NightData data);
        
        public static PrepareNight Make(LoadRoomData loadRoomData, LoadItemIds loadItemIds, IFloorRecreator floorRecreator)
        {
            return nightData =>
            {
                var roomData = loadRoomData();
                var itemIds = loadItemIds();

                var rngInitializer = SeedBasedInitializeRng;

                var floorPlanGenerator = FloorPlanGenerating.Make(roomData, rngInitializer);
                var floorGenerator = FloorGenerating.Make(itemIds, roomData);
                var nightRecreator = NightRecreating.Make(floorRecreator);

                Take(nightData.Seed)
                    .Map(floorPlanGenerator.Invoke)
                    .Map(floorGenerator.Invoke)
                    .Do(nightRecreator.Invoke);
            };
        }

    }

}