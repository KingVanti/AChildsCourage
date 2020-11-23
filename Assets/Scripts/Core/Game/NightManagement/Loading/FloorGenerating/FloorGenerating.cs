using AChildsCourage.Game.Floors;
using System.Linq;
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
            Take(floorPlan)
            .Map(roomLoader.Invoke)
            .Select(room => roomBuilder(floor, room))
            .ThenTake(floor)
            .Map(floorCreator.Invoke);

    }

}