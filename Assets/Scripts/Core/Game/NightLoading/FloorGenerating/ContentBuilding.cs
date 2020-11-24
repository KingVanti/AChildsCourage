using AChildsCourage.Game.Floors.RoomPersistance;
using System;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightLoading
{

    internal static class ContentBuilding
    {

        internal static ContentBuilder GetDefault(TileTransformer transformer)
        {
            return (content, floor) =>
            {
                var groundBuilder = GroundBuilding.GetDefault(transformer);
                var courageBuilder = CourageBuilding.GetDefault(transformer);
                var itemPickupBuilder = ItemPickupBuilding.GetDefault(transformer);

                return BuildContent(groundBuilder, courageBuilder, itemPickupBuilder, floor, content);
            };
        }


        private static FloorInProgress BuildContent(GroundBuilder groundBuilder, CourageBuilder courageBuilder, ItemPickupBuilder itemPickupBuilder, FloorInProgress floor, RoomContentData content)
        {
            Func<FloorInProgress, FloorInProgress> buildGround = f => groundBuilder(content.GroundData, f);
            Func<FloorInProgress, FloorInProgress> buildCourage = f => courageBuilder(content.CourageData, f);
            Func<FloorInProgress, FloorInProgress> buildItems = f => itemPickupBuilder(content.ItemData, f);

            return
                Take(floor)
                .Map(buildGround)
                .Map(buildCourage)
                .Map(buildItems);
        }

    }

}