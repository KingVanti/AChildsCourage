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
                var floor = new FloorInProgress();

                var roomBuilder = RoomBuilding.GetDefault();
                var floorCreator = FloorCreating.GetDefault();

                return Generate(floor, floorPlan, roomLoader, roomBuilder, floorCreator);
            };
        }


        internal static Floor Generate(FloorInProgress floor, FloorPlan floorPlan, RoomLoader roomLoader, RoomBuilder roomBuilder, FloorCreator floorCreator) =>
            Pipe(floorPlan)
            .Into(roomLoader.Invoke)
            .Then().AllInto(room => roomBuilder(floor, room))

            .ThenPipe(floor)
            .Into(floorCreator.Invoke);

    }

}