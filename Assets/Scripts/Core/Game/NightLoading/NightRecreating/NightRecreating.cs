namespace AChildsCourage.Game.NightLoading
{

    internal static partial class NightRecreating
    {

        internal static NightRecreator Make(IFloorRecreator floorRecreator)
        {
            return floor =>
            {
                floorRecreator.Recreate(floor);
            };
        }

    }

}