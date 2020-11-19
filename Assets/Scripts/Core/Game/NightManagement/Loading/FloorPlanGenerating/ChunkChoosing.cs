using System;
using System.Linq;
using System.Numerics;

using static AChildsCourage.F;
using static AChildsCourage.Game.NightManagement.Loading.FloorGenerationUtility;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class ChunkChoosing
    {

        private const float BaseWeight = 1;


        internal static ChunkChooser GetDefault(IRNG rng)
        {
            return builder => ChooseNextChunk(builder, rng);
        }


        private static ChunkPosition ChooseNextChunk(FloorPlanBuilder builder, IRNG rng)
        {


            var phase =
                Pipe(builder)
                .Into(CountRooms)
                .Then().Into(GetCurrentPhase);

            switch (phase)
            {
                case GenerationPhase.StartRoom:
                    return GetStartRoomChunk();

                case GenerationPhase.NormalRooms:
                case GenerationPhase.EndRoom:
                    return GetNextChunk(builder, rng);

                default:
                    throw new Exception("Invalid building phase!");
            }
        }

        internal static ChunkPosition GetStartRoomChunk()
        {
            return new ChunkPosition(0, 0);
        }

        private static ChunkPosition GetNextChunk(FloorPlanBuilder builder, IRNG rng)
        {
            if (HasReservedChunks(builder))
                return builder.ReservedChunks.GetWeightedRandom(GetChunkWeight, rng);

            throw new Exception("Could not find any more possible chunks!");
        }

        internal static bool HasReservedChunks(FloorPlanBuilder builder)
        {
            return builder.ReservedChunks.Any();
        }

        internal static float GetChunkWeight(ChunkPosition position)
        {
            return
                BaseWeight +
                CalculateDistanceWeight(position);
        }

        private static float CalculateDistanceWeight(ChunkPosition position)
        {
            var distance = new Vector2(position.X, position.Y).Length();

            return (float)Math.Pow(1f / distance, 2);
        }

    }

}