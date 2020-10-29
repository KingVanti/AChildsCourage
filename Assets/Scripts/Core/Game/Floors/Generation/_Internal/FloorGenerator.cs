namespace AChildsCourage.Game.Floors.Generation
{

    [Singleton]
    internal class FloorGenerator : IFloorGenerator
    {

        #region Methods

        public FloorPlan GenerateNew(int seed)
        {
            // TODO: Implement floor generation

            return new FloorPlan(new[]
            {
                new RoomAtPosition(0, new TilePosition(0, 0))
            });
        }

        #endregion

    }

}