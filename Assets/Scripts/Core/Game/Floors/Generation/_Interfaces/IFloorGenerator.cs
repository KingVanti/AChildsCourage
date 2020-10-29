namespace AChildsCourage.Game.Floors.Generation
{

    public interface IFloorGenerator
    {

        #region Methods

        FloorPlan GenerateNew(int seed);

        #endregion

    }

}