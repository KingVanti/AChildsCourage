using System.Collections.Generic;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.Items;
using static AChildsCourage.F;
using static AChildsCourage.Game.FloorGenerating;
using static AChildsCourage.Game.FloorPlanGenerating;
using static AChildsCourage.Game.MNightData;
using static AChildsCourage.Game.NightRecreating;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game
{

    public static class MNightPreparation
    {

        internal static PrepareNight PrepareNightWithRandomFloor(IEnumerable<RoomData> roomData, IEnumerable<ItemId> itemIds, RecreateNight recreateNight) =>
            nightData =>
                Take(nightData.Seed)
                    .Map(seed => GenerateFloorPlan(roomData, FromSeed(seed)))
                    .Map(floorPlan => GenerateFloor(floorPlan, itemIds, roomData))
                    .Do(recreateNight.Invoke);

        internal delegate void PrepareNight(NightData data);

    }

}