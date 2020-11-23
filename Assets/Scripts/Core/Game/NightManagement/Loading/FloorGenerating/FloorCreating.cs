using AChildsCourage.Game.Floors;

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
            var groundTilePosition = floor.GroundPositions;
            var walls = generateWalls(floor);
            var courageOrbPositions = chooseCouragePositions(floor);

            return new Floor(groundTilePosition, walls, courageOrbPositions);
        }

    }

}