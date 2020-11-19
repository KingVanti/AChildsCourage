using AChildsCourage.Game.Floors;
using System;
using System.Linq;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class DataBuilding
    {

        internal static DataBuilder GetDefault()
        {
            return BuildDataTiles;
        }


        internal static FloorBuilder BuildDataTiles(FloorBuilder builder, Tiles<DataTile> tiles, TileTransformer transformer)
        {
            Func<DataTile, DataTile> transformed = tile => TransformDataTile(tile, transformer);
            Action<DataTile> place = tile => PlaceDataTile(tile, builder);

            Pipe(tiles)
            .Select(transformed)
            .AllInto(place);

            return builder;
        }

        internal static DataTile TransformDataTile(DataTile dataTile, TileTransformer transformer)
        {
            var newPosition = transformer(dataTile.Position);

            return dataTile.With(newPosition);
        }

        internal static DataTile With(this DataTile dataTile, TilePosition position)
        {
            return new DataTile(position, dataTile.Type);
        }

        internal static void PlaceDataTile(DataTile dataTile, FloorBuilder builder)
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