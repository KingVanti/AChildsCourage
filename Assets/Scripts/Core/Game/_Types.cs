using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game
{

    internal delegate FloorPlan FloorPlanGenerator(int seed);

    internal delegate Floor FloorGenerator(FloorPlan floorPlan);

    internal delegate void NightRecreator(Floor floor);

    internal delegate void NightLoader(NightData data);

}