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
                var couragePositionChooser = CouragePositionChoosing.GetDefault();

                return CreateFloor(floor, wallGenerator, couragePositionChooser);
            };
        }


        internal static Floor CreateFloor(FloorInProgress floor, WallGenerator generateWalls, CouragePositionChooser chooseCouragePositions)
        {
            var groundTiles = floor.GroundPositions.Select(p => new GroundTile(p));
            var walls = generateWalls(floor);
            var courageOrbPositions = chooseCouragePositions(floor);

            return new Floor(groundTiles, walls, courageOrbPositions);
        }

    }

}