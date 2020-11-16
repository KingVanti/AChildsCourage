using Ninject;
using Ninject.Activation;
using Ninject.Parameters;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        private delegate ChunkPosition ChunkChooser(FloorPlanBuilder builder);

        private delegate RoomPassages RoomChooser(FloorPlanBuilder builder, ChunkPosition chunkPosition);

        public static FloorGenerator GetFloorGenerator(IContext context)
        {
            var kernel = context.Kernel;

            IRNG GetRNGFor(int seed) =>
                kernel.Get<IRNG>(new ConstructorArgument("seed", seed));

            IRoomPassagesRepository GetRoomPassagesRepository() =>
                kernel.Get<IRoomPassagesRepository>();

            ChunkChooser GetChunkChooser(IRNG rng) =>
                builder => ChooseNextChunk(builder, rng);

            RoomChooser GetRoomChooser(IRoomPassagesRepository roomPassagesRepository, IRNG rng) =>
                (builder, chunkPosition) => ChooseNextRoom(chunkPosition, builder, roomPassagesRepository, rng);

            return seed =>
            {
                var rng = GetRNGFor(seed);
                var roomPassagesRepository = GetRoomPassagesRepository();

                ChunkChooser chunkChooser = GetChunkChooser(rng);
                RoomChooser roomChooser = GetRoomChooser(roomPassagesRepository, rng);

                return Generate(chunkChooser, roomChooser);
            };
        }
    }

}