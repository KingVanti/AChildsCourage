using System;
using static AChildsCourage.M;
using static AChildsCourage.Game.Floors.Gen.ChunkLayout;
using static AChildsCourage.Rng;

namespace AChildsCourage.Game.Floors.Gen
{

    internal static class ChunkLayoutGen
    {

        private const float NoWeight = 0;


        internal static ChunkLayout GenerateChunkLayout(FloorGenParams @params)
        {
            var rng = RngFromSeed(@params.Seed);

            ChunkLayout OccupyNextChunk(ChunkLayout layout)
            {
                float CalculateConnectivityWeight(Chunk position)
                {
                    var directConnectionCount = position.Map(CountDirectConnections, layout);
                    var indirectConnectionCount = position.Map(CountIndirectConnections, layout);

                    var directConnectionWeight = directConnectionCount > 1 ? @params.ClumpingFactor : NoWeight;
                    var indirectConnectionWeight = indirectConnectionCount.Map(Times, @params.ClumpingFactor);

                    return directConnectionWeight + indirectConnectionWeight;
                }

                float CalculateWeight(Chunk position) =>
                    CalculateConnectivityWeight(position);

                return layout.Map(GetPossibleNextChunks)
                             .TryGetWeightedRandom(CalculateWeight, rng, () => throw new Exception("No possible chunks remaining!"))
                             .Map(OccupyIn, layout);
            }


            var remainingRoomCount = @params.RoomCount - BaseChunkCount;

            return BaseChunkLayout
                .Cycle(OccupyNextChunk, remainingRoomCount);
        }

    }

}