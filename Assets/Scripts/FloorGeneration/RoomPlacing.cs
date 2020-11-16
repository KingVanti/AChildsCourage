using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        internal static void PlaceRoom(this FloorPlanBuilder builder, ChunkPosition chunkPosition, RoomPassages roomPassages)
        {
            builder.RoomsByChunks.Add(chunkPosition, roomPassages);
            builder.ReservedChunks.Remove(chunkPosition);

            builder.ReserveChunksAround(chunkPosition);
        }

        private static void ReserveChunksAround(this FloorPlanBuilder builder, ChunkPosition chunkPosition)
        {
            var reservablePositions = builder.GetReservablePositionsAround(chunkPosition);

            foreach (var rereservablePosition in reservablePositions)
                builder.ReservedChunks.Add(rereservablePosition);
        }

        private static IEnumerable<ChunkPosition> GetReservablePositionsAround(this FloorPlanBuilder builder, ChunkPosition position)
        {
            return
                GetSurroundingPositions(position)
                .Where(p => builder.CanReserve(p));
        }

        internal static bool CanReserve(this FloorPlanBuilder builder, ChunkPosition position)
        {
            return !builder.HasReserved(position) && builder.IsEmpty(position) && builder.AnyPassagesLeadInto(position);
        }

    }

}
