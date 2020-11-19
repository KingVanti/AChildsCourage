using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AChildsCourage.FloorTilesBuilding.Tests")]

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static FloorTiles Build(FloorPlan floorPlan, LoadRoomsFor roomLoader, IRNG rng)
        {
            var rooms = roomLoader(floorPlan);

            return BuildFloorFrom(rooms, rng);
        }

        private static FloorTiles BuildFloorFrom(RoomsForFloor rooms, IRNG rng)
        {
            var builder = new FloorTilesBuilder();

            rooms.ForEach(r => r.BuildInto(builder));

            builder.GenerateWalls();

            return builder.GetFloor(rng);
        }

    }

}