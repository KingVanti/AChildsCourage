using System;
using System.Numerics;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        private const float BaseWeight = 1;

        private static ChunkPosition ChooseNextChunk(FloorPlanBuilder builder, IRNG rng)
        {
            var phase = builder.CountRooms().GetCurrentPhase();

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
            if (builder.HasReservedChunks())
                return builder.ReservedChunks.GetWeightedRandom(GetChunkWeight, rng);

            throw new Exception("Could not find any more possible chunks!");
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