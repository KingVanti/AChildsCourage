using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightManagement.Loading
{

    public delegate FloorPlan FloorPlanGenerator(int seed);

    public delegate Floor FloorGenerator(FloorPlan floorPlan);

    public delegate void NightRecreator(Floor floor);

}