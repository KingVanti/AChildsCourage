using static AChildsCourage.Game.Floors.MFloor;

namespace AChildsCourage.Game
{

    internal static class MNightRecreating
    {

        internal static RecreateNight Make(IFloorRecreator floorRecreator) => floorRecreator.Recreate;

        internal delegate void RecreateNight(Floor floor);

    }

}