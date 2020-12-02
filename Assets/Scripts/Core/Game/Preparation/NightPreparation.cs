using static AChildsCourage.F;
using static AChildsCourage.Game.FloorGenerating;
using static AChildsCourage.Game.FloorPlanGenerating;
using static AChildsCourage.Game.MNightData;
using static AChildsCourage.Game.NightRecreating;

namespace AChildsCourage.Game
{

    public static class MNightPreparation
    {

        internal static PrepareNight PrepareNightWithRandomFloor(GenerateFloorPlan generateFloorPlan, GenerateFloor generateFloor, RecreateNight recreateNight) =>
            nightData =>
                Take(nightData.Seed)
                    .Map(generateFloorPlan.Invoke)
                    .Map(generateFloor.Invoke)
                    .Do(recreateNight.Invoke);

        internal delegate void PrepareNight(NightData data);

    }

}