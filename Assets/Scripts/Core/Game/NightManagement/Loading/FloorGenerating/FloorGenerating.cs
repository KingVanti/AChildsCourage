using AChildsCourage.Game.Floors;
using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class FloorGenerating
    {

        internal static FloorGenerator GetDefault(RoomLoader roomLoader)
        {
            return floorPlan =>
            {
                var builder = new FloorBuilder();

                var roomBuilder = RoomBuilding.GetDefault();
                var floorCreator = FloorCreating.GetDefault();

                return Generate(builder, floorPlan, roomLoader, roomBuilder, floorCreator);
            };
        }


        internal static Floor Generate(FloorBuilder builder, FloorPlan floorPlan, RoomLoader roomLoader, RoomBuilder roomBuilder, FloorCreator floorCreator) =>
            Pipe(floorPlan)
            .Into(roomLoader.Invoke)
            .Then().AllInto(room => roomBuilder(builder, room))

            .ThenPipe(builder)
            .Into(floorCreator.Invoke);

    }

}