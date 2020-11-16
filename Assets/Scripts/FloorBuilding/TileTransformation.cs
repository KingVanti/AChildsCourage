namespace AChildsCourage.Game.Floors
{

    public static partial class FloorBuilding
    {

        internal static GroundTile Transform(this TilePositionTransformer transformer, GroundTile groundTile)
        {
            var newPosition = groundTile.Position + transformer.TileOffset;

            return new GroundTile(newPosition, groundTile.DistanceToWall, groundTile.AOIIndex);
        }

    }

}