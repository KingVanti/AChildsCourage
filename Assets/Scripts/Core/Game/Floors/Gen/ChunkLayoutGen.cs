using static AChildsCourage.F;
using static AChildsCourage.Game.Floors.Gen.ChunkLayout;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game.Floors.Gen
{

    public static class ChunkLayoutGen
    {

        private const float NoWeight = 0;


        public static ChunkLayout GenerateChunkLayout(FloorGenParams @params)
        {
            var rng = RngFromSeed(@params.Seed);

            ChunkLayout OccupyNextChunk(ChunkLayout layout)
            {
                float CalculateConnectivityWeight(ChunkPosition position)
                {
                    var directConnectionCount = position.Map(CountDirectConnections, layout);
                    var indirectConnectionCount = position.Map(CountIndirectConnections, layout);

                    var directConnectionWeight = directConnectionCount > 1 ? @params.ClumpingFactor : NoWeight;
                    var indirectConnectionWeight = indirectConnectionCount.Times(@params.ClumpingFactor);

                    return directConnectionWeight + indirectConnectionWeight;
                }

                float CalculateWeight(ChunkPosition position) =>
                    CalculateConnectivityWeight(position);

                return layout
                       .Map(GetPossibleNextChunks)
                       .GetWeightedRandom(CalculateWeight, rng)
                       .Map(OccupyIn, layout);
            }


            return AggregateTimes(BaseChunkLayout, OccupyNextChunk, @params.RoomCount - BaseChunkCount);
        }

    }

}