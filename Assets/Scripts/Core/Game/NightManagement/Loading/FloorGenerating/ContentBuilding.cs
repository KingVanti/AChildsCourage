using AChildsCourage.Game.Floors.RoomPersistance;
using System;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class ContentBuilding
    {

        internal static ContentBuilder GetDefault(TileTransformer transformer)
        {
            return (content, floor) =>
            {
                var groundBuilder = GroundBuilding.GetDefault(transformer);
                var courageBuilder = CourageBuilding.GetDefault(transformer);

                return BuildContent(groundBuilder, courageBuilder, floor, content);
            };
        }


        private static FloorInProgress BuildContent(GroundBuilder groundBuilder, CourageBuilder courageBuilder, FloorInProgress floor, RoomContentData content)
        {
            Func<FloorInProgress, FloorInProgress> buildGround = f => groundBuilder(content.GroundData, f);
            Func<FloorInProgress, FloorInProgress> buildData = f => courageBuilder(content.CourageData, f);

            return
                Take(floor)
                .Map(buildGround)
                .Map(buildData);
        }

    }

}