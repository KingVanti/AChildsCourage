using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public readonly struct GroundTileData
    {

        public TilePosition Position { get; }

        public int DistanceToWall { get; }

        public int AOIIndex { get; }


        public GroundTileData(TilePosition position, int distanceToWall, int aOIIndex)
        {
            Position = position;
            DistanceToWall = distanceToWall;
            AOIIndex = aOIIndex;
        }

    }

}