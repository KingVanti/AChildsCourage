using System;
using System.Linq;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MFloorGenerating.MFloorLayoutBuilder;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MChunkChoosing
        {

            private const float BaseWeight = 1;


            public static Func<FloorLayoutBuilder, CreateRng, GenerationParameters, ChunkPosition> ChooseNextChunk =>
                (builder, createRng, parameters) =>
                {
                    switch (GetCurrentPhase(builder, parameters))
                    {
                        case LayoutGenerationPhase.StartRoom:
                            return GetStartRoomChunk();

                        case LayoutGenerationPhase.NormalRooms:
                        case LayoutGenerationPhase.EndRoom:
                            return ChooseRandomNextChunk(builder, createRng);

                        default:
                            throw new Exception("Invalid building phase!");
                    }
                };

            private static Func<ChunkPosition> GetStartRoomChunk =>
                () =>
                    new ChunkPosition(0, 0);

            private static Func<FloorLayoutBuilder, CreateRng, ChunkPosition> ChooseRandomNextChunk =>
                (builder, createRng) =>
                {
                    if (!HasReservedChunks(builder))
                        throw new Exception("Could not find any more possible chunks!");

                    float WeightFunction(ChunkPosition chunk) => CalculateChunkWeight(builder, chunk);

                    return builder.ReservedChunks.GetWeightedRandom(WeightFunction, createRng);
                };

            // [1 .. 27]
            private static float CalculateChunkWeight(FloorLayoutBuilder builder, ChunkPosition chunk) =>
                BaseWeight +
                CalculateDistanceWeight(chunk) +
                CalculateConnectivityWeight(builder, chunk);

            // [0 .. 10]
            private static float CalculateDistanceWeight(ChunkPosition chunk) =>
                GetDistanceToOrigin(chunk)
                    .Clamp(1, 5)
                    .Remap(1, 5, 10, 0);

            // [0 .. 15]
            private static float CalculateConnectivityWeight(FloorLayoutBuilder builder, ChunkPosition chunk) =>
                GetAdjacentChunks(chunk)
                    .Count(c => IsOccupied(builder, c))
                    .Minus(1)
                    .Times(5);

        }

    }

}