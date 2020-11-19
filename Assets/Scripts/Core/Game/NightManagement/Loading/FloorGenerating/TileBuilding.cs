using AChildsCourage.Game.Floors;
using System;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class TileBuilding
    {

        internal static TileBuilder GetDefault()
        {
            return (builder, tiles, transformer) =>
            {
                var groundBuilder = GroundBuilding.GetDefault();
                var dataBuilder = DataBuilding.GetDefault();

                return Build(builder, tiles, transformer, groundBuilder, dataBuilder);
            };
        }


        private static FloorBuilder Build(FloorBuilder builder, RoomTiles tiles, TileTransformer transformer, GroundBuilder groundBuilder, DataBuilder dataBuilder)
        {
            Func<FloorBuilder, FloorBuilder> buildGround = b => groundBuilder(b, tiles.GroundTiles, transformer);
            Func<FloorBuilder, FloorBuilder> buildData = b => dataBuilder(b, tiles.DataTiles, transformer);

            return
                Pipe(builder)
                .Into(buildGround)
                .Then().Into(buildData);
        }

    }

}