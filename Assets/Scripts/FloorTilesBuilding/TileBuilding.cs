using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static void Build(this FloorTilesBuilder builder, RoomInChunk room)
        {
            var tiles = room.Room.Tiles;
            var transformer = CreateTransformerFor(room);

            builder.Build(tiles, transformer);
        }

        private static void Build(this FloorTilesBuilder builder, RoomTiles tiles, TilePositionTransformer transformer)
        {
            builder.BuildGroundTiles(tiles.GroundTiles, transformer);
        }

        private static void BuildGroundTiles(this FloorTilesBuilder builder, Tiles<GroundTile> groundTiles, TilePositionTransformer transformer)
        {
            groundTiles
                .Select(transformer.Transform)
                .ForEach(builder.PlaceGroundTile);
        }

        internal static void PlaceGroundTile(this FloorTilesBuilder builder, GroundTile groundTile)
        {
            builder.GroundPositions.Add(groundTile.Position);
        }

    }

}