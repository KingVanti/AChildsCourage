using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AChildsCourage.FloorBuilding.Tests")]

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorBuilding
    {

        private static Floor Build(FloorPlan floorPlan, RoomLoader roomLoader)
        {
            var rooms = roomLoader(floorPlan);

            return BuildFloorFrom(rooms);
        }

        private static Floor BuildFloorFrom(RoomsForFloor rooms)
        {
            var builder = new FloorBuilder();

            foreach (var room in rooms)
                builder.Build(room);

            builder.GenerateWalls();

            return builder.GetFloor();
        }

    }

}