using System;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static void BuildInto(this RoomInChunk room, FloorTilesBuilder builder)
        {
            var tiles = room.Room.Tiles;
            var transformer = CreateTransformerFor(room.Transform);

            tiles.BuildInto(builder, transformer);
        }

        private static void BuildInto(this RoomTiles tiles, FloorTilesBuilder builder, TilePositionTransformer transformer)
        {
            tiles.GroundTiles.Build(transformer.TransformGroundTile, builder.PlaceGround);
            tiles.DataTiles.Build(transformer.TransformDataTile, builder.PlaceDataTile);
        }

        private static void Build(this Tiles<GroundTile> groundTiles, Func<GroundTile, GroundTile> transform, Action<GroundTile> place)
        {
            groundTiles
                .Select(transform)
                .ForEach(place);
        }

        internal static void PlaceGround(this FloorTilesBuilder builder, GroundTile groundTile)
        {
            builder.GroundPositions.Add(groundTile.Position);
        }

        private static void Build(this Tiles<DataTile> dataTiles, Func<DataTile, DataTile> transform, Action<DataTile> place)
        {
            dataTiles
                .Select(transform)
                .ForEach(place);
        }

        internal static void PlaceDataTile(this FloorTilesBuilder builder, DataTile dataTile)
        {
            switch (dataTile.Type)
            {
                case DataTileType.CourageOrb:
                    builder.CourageOrbPositions.Add(dataTile.Position);
                    break;
                case DataTileType.CourageSpark:
                    builder.CourageSparkPositions.Add(dataTile.Position);
                    break;
            }
        }

    }

}