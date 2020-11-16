using Newtonsoft.Json;

namespace AChildsCourage.Game.Floors
{

    public readonly struct GroundTile
    {

        #region Properties

        public TilePosition Position { get; }

        public int DistanceToWall { get; }

        public int AOIIndex { get; }

        #endregion

        #region Constructors

        internal GroundTile(int x, int y, int distanceToWall, int aoiIndex)
        {
            Position = new TilePosition(x, y);
            DistanceToWall = distanceToWall;
            AOIIndex = aoiIndex;
        }

        [JsonConstructor]
        public GroundTile(TilePosition position, int distanceToWall, int aoiIndex)
        {
            Position = position;
            DistanceToWall = distanceToWall;
            AOIIndex = aoiIndex;
        }

        #endregion

    }

}