using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class FloorCreating
    {

        internal static FloorCreator GetDefault()
        {
            return builder =>
            {
                var wallGenerator = WallGenerating.GetDefault();
                var couragePositionChooser = CouragePositionChoosing.GetDefault();

                return CreateFloor(builder, wallGenerator, couragePositionChooser);
            };
        }


        internal static Floor CreateFloor(FloorBuilder builder, WallGenerator wallGenerator, CouragePositionChooser couragePositionChooser)
        {
            var groundTilePosition = builder.GroundPositions;
            var walls = wallGenerator(builder);
            var courageOrbPositions = couragePositionChooser(builder);

            return new Floor(groundTilePosition, walls, courageOrbPositions);
        }

    }

}