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


        internal static Floor Generate(FloorInProgress floor, FloorPlan floorPlan, RoomLoader loadRooms, RoomBuilder buildRoom, FloorCreator createFloor)
        {
            Take(floorPlan)
            .Map(loadRooms.Invoke)
            .ForEach(room => buildRoom(floor, room));

            return
                Take(floor)
                .Map(createFloor.Invoke);
        }

    }

}