using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightLoading
{

    internal delegate FloorPlan FloorPlanGenerator(int seed);

    internal delegate Floor FloorGenerator(FloorPlan floorPlan);

    internal delegate void NightRecreator(Floor floor);

}