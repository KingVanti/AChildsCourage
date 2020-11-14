namespace AChildsCourage.Game.Floors.Generation
{

    internal interface IFloorGenerator
    {

        #region Methods

        FloorPlan GenerateNew(int seed);

        #endregion

    }

}