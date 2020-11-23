using AChildsCourage.Game.Floors;
using System.Linq;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class FloorCreating
    {

        internal static FloorCreator GetDefault()
        {
            return floor =>
            {
                var wallGenerator = WallGenerating.GetDefault();
                var couragePositionChooser = CouragePickupCreating.GetDefault();

                return CreateFloor(floor, wallGenerator, couragePositionChooser);
            };
        }


        internal static Floor CreateFloor(FloorInProgress floor, WallGenerator generateWalls, CouragePickupCreator chooseCouragePickups)
        {
            var groundTiles = floor.GroundPositions.Select(p => new GroundTile(p));
            var walls = generateWalls(floor);
            var couragePickups = chooseCouragePickups(floor);

            return new Floor(groundTiles, walls, couragePickups);
        }

    }

}