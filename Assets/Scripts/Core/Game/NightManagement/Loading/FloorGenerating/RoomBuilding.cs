using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class RoomBuilding
    {

        internal static RoomBuilder GetDefault()
        {
            return (floor, room) =>
            {
                var transform = ToChunkTransform(room.Transform);

                TileTransformer transformer = TileTransforming.GetDefault(transform);
                ContentBuilder tileBuilder = ContentBuilding.GetDefault(transformer);
                
                return BuildRoom(tileBuilder, floor, room);
            };
        }


        private static FloorInProgress BuildRoom(ContentBuilder tileBuilder, FloorInProgress floor, RoomForFloor room)
        {
            return tileBuilder(room.Content, floor);
        }

        internal static ChunkTransform ToChunkTransform(RoomTransform transform)
        {
            var chunkCenter = GetChunkCenter(transform.Position);
            var chunkCorner = GetChunkCorner(transform.Position);

            return new ChunkTransform(transform.RotationCount, transform.IsMirrored, chunkCorner, chunkCenter);
        }

        internal static TilePosition GetChunkCenter(ChunkPosition chunkPosition)
        {
            return new TilePosition(
                (chunkPosition.X * ChunkPosition.ChunkSize) + ChunkPosition.ChunkExtent,
                (chunkPosition.Y * ChunkPosition.ChunkSize) + ChunkPosition.ChunkExtent);
        }

        internal static TilePosition GetChunkCorner(ChunkPosition chunkPosition)
        {
            return new TilePosition(
                chunkPosition.X * ChunkPosition.ChunkSize,
                chunkPosition.Y * ChunkPosition.ChunkSize);
        }

    }

}