using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game
{

    internal delegate Floor GenerateFloor(FloorPlan floorPlan);

    internal delegate void RecreateNight(Floor floor);

}