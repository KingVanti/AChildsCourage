using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightLoading
{

    internal static class NightRecreating
    {

        internal static NightRecreator GetDefault(IFloorRecreator floorRecreator)
        {
            return floor =>
            {
                Recreate(floor, floorRecreator);
            };
        }


        internal static void Recreate(Floor floor, IFloorRecreator floorRecreator)
        {
            floorRecreator.Recreate(floor);
        }

    }

}