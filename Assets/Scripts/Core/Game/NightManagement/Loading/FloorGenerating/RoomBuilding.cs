using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class RoomBuilding
    {

        internal static RoomBuilder GetDefault()
        {
            return (builder, room) =>
            {
                var transform = ToChunkTransform(room.Transform);

                TileBuilder tileBuilder = TileBuilding.GetDefault();
                TileTransformer transformer = TileTransforming.GetDefault(transform);

                return BuildRoom(builder, room, tileBuilder, transformer);
            };
        }


        private static FloorBuilder BuildRoom(FloorBuilder builder, RoomForFloor room, TileBuilder tileBuilder, TileTransformer transformer)
        {
            return tileBuilder(builder, room.Room.Tiles, transformer);
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