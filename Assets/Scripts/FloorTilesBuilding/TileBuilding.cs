using System;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static void Build(this FloorTilesBuilder builder, RoomInChunk room)
        {
            var tiles = room.Room.Tiles;
            var transformer = CreateTransformerFor(room.Transform);

            tiles.Build(transformer.Transform, builder.Place);
        }

        private static void Build(this RoomTiles tiles, Func<GroundTile, GroundTile> transform, Action<GroundTile> place)
        {
            tiles.GroundTiles.Build(transform, place);
        }

        private static void Build(this Tiles<GroundTile> groundTiles, Func<GroundTile, GroundTile> transform, Action<GroundTile> place)
        {
            groundTiles
                .Select(transform)
                .ForEach(place);
        }

        internal static void Place(this FloorTilesBuilder builder, GroundTile groundTile)
        {
            builder.GroundPositions.Add(groundTile.Position);
        }

    }

}