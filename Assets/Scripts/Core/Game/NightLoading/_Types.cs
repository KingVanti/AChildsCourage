using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightLoading
{
    
    internal delegate Floor GenerateFloor(FloorPlan floorPlan);

    internal delegate void RecreateNight(Floor floor);

}