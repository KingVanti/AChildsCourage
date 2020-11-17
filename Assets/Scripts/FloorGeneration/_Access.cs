using Ninject;
using Ninject.Activation;
using Ninject.Parameters;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        public static GenerateFloor GetFloorGenerator(IContext context)
        {
            var kernel = context.Kernel;

            IRNG GetRNGFor(int seed) =>
                kernel.Get<IRNG>(new ConstructorArgument("seed", seed));

            IRoomPassagesRepository GetRoomPassagesRepository() =>
                kernel.Get<IRoomPassagesRepository>();

            ChooseChunk ChooseChunksWith(IRNG rng) =>
                builder => ChooseNextChunk(builder, rng);

            ChooseRoom ChooseRoomsWith(IRoomPassagesRepository roomPassagesRepository, IRNG rng) =>
                (builder, chunkPosition) => ChooseNextRoom(chunkPosition, builder, roomPassagesRepository, rng);

            return seed =>
            {
                var rng = GetRNGFor(seed);
                var roomPassagesRepository = GetRoomPassagesRepository();

                ChooseChunk chooseChunk = ChooseChunksWith(rng);
                ChooseRoom chooseRoom = ChooseRoomsWith(roomPassagesRepository, rng);

                return Generate(chooseChunk, chooseRoom);
            };
        }
    }

}