using AChildsCourage.Game.Floors.RoomPersistance;
using System;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class TileBuilding
    {

        internal static ContentBuilder GetDefault()
        {
            return (builder, content, transformer) =>
            {
                var groundBuilder = GroundBuilding.GetDefault();
                var courageBuilder = CourageBuilding.GetDefault();

                return Build(builder, content, transformer, groundBuilder, courageBuilder);
            };
        }


        private static FloorBuilder Build(FloorBuilder builder, RoomContentData content, TileTransformer transformer, GroundBuilder groundBuilder, CourageBuilder courageBuilder)
        {
            Func<FloorBuilder, FloorBuilder> buildGround = b => groundBuilder(b, content.GroundData, transformer);
            Func<FloorBuilder, FloorBuilder> buildData = b => courageBuilder(b, content.CourageData, transformer);

            return
                Pipe(builder)
                .Into(buildGround)
                .Then().Into(buildData);
        }

    }

}