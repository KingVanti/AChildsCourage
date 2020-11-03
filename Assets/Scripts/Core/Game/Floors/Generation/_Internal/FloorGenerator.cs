namespace AChildsCourage.Game.Floors.Generation
{

    [Singleton]
    internal class FloorGenerator : IFloorGenerator
    {

        #region Methods

        public FloorPlan GenerateNew(int seed)
        {
            var session = StartSession(seed);

            return session.Generate();
        }

        private IGenerationSession StartSession(int seed)
        {
            // TODO: Add RNG instance
            var rng = (IRNG)null;
            var chunkGrid = new ChunkGrid();

            return new GenerationSession(rng, chunkGrid);
        }

        #endregion

    }

}