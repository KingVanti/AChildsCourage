namespace AChildsCourage.Game.Floors.Generation
{

    [Singleton]
    internal class FloorGenerator : IFloorGenerator
    {

        #region Fields

        private readonly IRoomInfoRepository roomInfoRepository;

        #endregion

        #region Constructors

        public FloorGenerator(IRoomInfoRepository roomInfoRepository)
        {
            this.roomInfoRepository = roomInfoRepository;
        }

        #endregion

        #region Methods

        public FloorPlan GenerateNew(int seed)
        {
            var session = StartSession(seed);

            return session.Generate();
        }

        private IGenerationSession StartSession(int seed)
        {
            var rng = new RNG(seed);
            var chunkGrid = new ChunkGrid();

            return new GenerationSession(rng, chunkGrid, roomInfoRepository);
        }

        #endregion

    }

}