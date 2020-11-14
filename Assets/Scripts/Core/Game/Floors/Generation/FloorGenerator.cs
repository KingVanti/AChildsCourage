using Ninject;
using Ninject.Parameters;

namespace AChildsCourage.Game.Floors.Generation
{

    [Singleton]
    internal class FloorGenerator : IFloorGenerator
    {

        #region Fields

        private readonly IRoomPassagesRepository roomInfoRepository;
        private readonly IKernel kernel;

        #endregion

        #region Constructors

        internal FloorGenerator(IRoomPassagesRepository roomInfoRepository, IKernel kernel)
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
            var rng = kernel.Get<IRNG>(new ConstructorArgument("seed", seed, false));
            var chunkGrid = kernel.Get<IChunkGrid>();

            return new GenerationSession(rng, chunkGrid, roomInfoRepository);
        }

        #endregion

    }

}