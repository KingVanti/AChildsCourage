using System.Linq;
using static AChildsCourage.Game.Floors.Gen.ChunkLayout;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game.Floors.Gen
{

    public static class ChunkLayoutGen
    {

        private const float BaseWeight = 1;
        private const float NoWeight = 0;


        public static ChunkLayout GenerateChunkLayout(FloorGenParams @params)
        {
            var rng = RngFromSeed(@params.Seed);

            ChunkLayout OccupyNextChunk(ChunkLayout layout) =>
                layout
                    .Map(OccupyChunkIn, layout.Map(ChooseNextChunk));

            ChunkPosition ChooseNextChunk(ChunkLayout layout)
            {
                float ConnectivityWeight(ChunkPosition position)
                {
                    var directConnectionCount = GetAdjacentChunks(position)
                        .Count(p => IsOccupiedIn(layout, p));

                    var indirectConnectionCount = GetDiagonalAdjacentChunks(position)
                        .Count(p => IsOccupiedIn(layout, p));

                    return (directConnectionCount > 1 ? @params.ClumpingFactor : NoWeight) +
                           indirectConnectionCount.Times(@params.ClumpingFactor);
                }

                float TotalWeight(ChunkPosition position) =>
                    BaseWeight +
                    ConnectivityWeight(position);

                return layout
                       .Map(GetPossibleNextChunks)
                       .GetWeightedRandom(TotalWeight, rng);
            }

            return BaseChunkLayout.For(@params.RoomCount - BaseChunkCount, OccupyNextChunk);
        }

    }

}