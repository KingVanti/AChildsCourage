using AChildsCourage.Game.Floors.RoomPersistance;
using System;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class TileBuilding
    {

        internal static ContentBuilder GetDefault()
        {
            return (floor, content, transformer) =>
            {
                var groundBuilder = GroundBuilding.GetDefault();
                var courageBuilder = CourageBuilding.GetDefault();

                return Build(floor, content, transformer, groundBuilder, courageBuilder);
            };
        }


        private static FloorInProgress Build(FloorInProgress floor, RoomContentData content, TileTransformer transformer, GroundBuilder groundBuilder, CourageBuilder courageBuilder)
        {
            Func<FloorInProgress, FloorInProgress> buildGround = b => groundBuilder(b, content.GroundData, transformer);
            Func<FloorInProgress, FloorInProgress> buildData = b => courageBuilder(b, content.CourageData, transformer);

            return
                Pipe(floor)
                .Into(buildGround)
                .Then().Into(buildData);
        }

    }

}