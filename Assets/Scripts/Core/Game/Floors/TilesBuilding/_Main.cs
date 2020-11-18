using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AChildsCourage.FloorTilesBuilding.Tests")]

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static FloorTiles Build(FloorPlan floorPlan, LoadRoomsFor roomLoader)
        {
            var rooms = roomLoader(floorPlan);

            return BuildFloorFrom(rooms);
        }

        private static FloorTiles BuildFloorFrom(RoomsForFloor rooms)
        {
            var builder = new FloorTilesBuilder();

            rooms.ForEach(r => r.BuildInto(builder));

            builder.GenerateWalls();

            return builder.GetFloor();
        }

    }

}