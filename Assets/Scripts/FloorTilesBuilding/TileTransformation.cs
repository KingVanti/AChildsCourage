namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private static GroundTile Transform(this TilePositionTransformer transformer, GroundTile groundTile)
        {
            var newPosition = transformer.Transform(groundTile.Position);

            return groundTile.With(newPosition);
        }

        internal static TilePosition Transform(this TilePositionTransformer transformer,TilePosition position)
        {
            return position + transformer.TileOffset;
        }

        internal static GroundTile With(this GroundTile groundTile, TilePosition position)
        {
            return new GroundTile(position, groundTile.DistanceToWall, groundTile.AOIIndex);
        }

    }

}