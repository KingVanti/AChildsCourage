namespace AChildsCourage.Game.NightLoading
{

    internal static class NightRecreating
    {

        internal static NightRecreator Make(IFloorRecreator floorRecreator)
        {
            return floor => { floorRecreator.Recreate(floor); };
        }

    }

}