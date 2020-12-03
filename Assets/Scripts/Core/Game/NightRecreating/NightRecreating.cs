using static AChildsCourage.Game.Floors.MFloor;

namespace AChildsCourage.Game
{

    internal static class NightRecreating
    {
        
        internal delegate void RecreateNight(Floor floor);
        
        internal static RecreateNight Make(IFloorRecreator floorRecreator)
        {
            return floor => { floorRecreator.Recreate(floor); };
        }

    }

}