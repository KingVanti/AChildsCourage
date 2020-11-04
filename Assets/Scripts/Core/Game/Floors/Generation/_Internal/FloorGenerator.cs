using Ninject;
using Ninject.Parameters;

namespace AChildsCourage.Game.Floors.Generation
{

    [Singleton]
    internal class FloorGenerator : IFloorGenerator
    {

        #region Fields

        private readonly IRoomInfoRepository roomInfoRepository;
        private readonly IKernel kernel;

        #endregion

        #region Constructors

        public FloorGenerator(IRoomInfoRepository roomInfoRepository, IKernel kernel)
        {
            this.roomInfoRepository = roomInfoRepository;
            this.kernel = kernel;
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
            var rng = kernel.Get<IRNG>(new Parameter("seed", seed, true));
            var chunkGrid = kernel.Get<IChunkGrid>();

            return new GenerationSession(rng, chunkGrid, roomInfoRepository);
        }

        #endregion

    }

}