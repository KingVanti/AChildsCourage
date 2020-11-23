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


        internal static Floor CreateFloor(FloorInProgress floor, WallGenerator wallGenerator, CouragePositionChooser couragePositionChooser)
        {
            var groundTilePosition = floor.GroundPositions;
            var walls = wallGenerator(floor);
            var courageOrbPositions = couragePositionChooser(floor);

            return new Floor(groundTilePosition, walls, courageOrbPositions);
        }

    }

}