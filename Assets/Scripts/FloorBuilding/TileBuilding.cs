namespace AChildsCourage.Game.Floors
{

    public static partial class FloorBuilding
    {

        private static void Build(this FloorBuilder builder, RoomInChunk room)
        {
            var transformer = CreateTransformerFor(room);

            builder.Build(room.Room.Tiles, transformer);
        }

        private static void Build(this FloorBuilder builder, RoomTiles tiles, TilePositionTransformer transformer)
        {
            builder.BuildGroundTiles(tiles.GroundTiles, transformer);
        }

        internal static void BuildGroundTiles(this FloorBuilder builder, Tiles<GroundTile> groundTiles, TilePositionTransformer transformer)
        {
            foreach (var groundTile in groundTiles)
            {
                var transformed = transformer.Transform(groundTile);

                builder.PlaceGroundTile(transformed);
            }
        }

        internal static GroundTile Transform(this TilePositionTransformer transformer, GroundTile groundTile)
        {
            var newPosition = groundTile.Position + transformer.TileOffset;

            return new GroundTile(newPosition, groundTile.DistanceToWall, groundTile.AOIIndex);
        }

        internal static void PlaceGroundTile(this FloorBuilder builder, GroundTile groundTile)
        {
            builder.GroundTilePositions.Add(groundTile.Position);
        }

    }

}