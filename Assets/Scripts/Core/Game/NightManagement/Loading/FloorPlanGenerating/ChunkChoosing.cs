using System;
using System.Linq;
using System.Numerics;

using static AChildsCourage.F;
using static AChildsCourage.Game.NightManagement.Loading.FloorPlanGeneratingUtility;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class ChunkChoosing
    {

        private const float BaseWeight = 1;


        internal static ChunkChooser GetDefault(IRNG rng)
        {
            return floorPlan => ChooseNextChunk(floorPlan, rng);
        }


        private static ChunkPosition ChooseNextChunk(FloorPlanInProgress floorPlan, IRNG rng)
        {


            var phase =
                Take(floorPlan)
                .Map(CountRooms)
                .Map(GetCurrentPhase);

            switch (phase)
            {
                case GenerationPhase.StartRoom:
                    return GetStartRoomChunk();

                case GenerationPhase.NormalRooms:
                case GenerationPhase.EndRoom:
                    return GetNextChunk(floorPlan, rng);

                default:
                    throw new Exception("Invalid building phase!");
            }
        }

        internal static ChunkPosition GetStartRoomChunk()
        {
            return new ChunkPosition(0, 0);
        }

        private static ChunkPosition GetNextChunk(FloorPlanInProgress floorPlan, IRNG rng)
        {
            if (HasReservedChunks(floorPlan))
                return floorPlan.ReservedChunks.GetWeightedRandom(GetChunkWeight, rng);

            throw new Exception("Could not find any more possible chunks!");
        }

        internal static bool HasReservedChunks(FloorPlanInProgress floorPlan)
        {
            return floorPlan.ReservedChunks.Any();
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