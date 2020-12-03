using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistance
{

    public readonly struct GroundTileData
    {

        public TilePosition Position { get; }

        public int DistanceToWall { get; }

        public int AoiIndex { get; }


        public GroundTileData(TilePosition position, int distanceToWall, int aOiIndex)
        {
            Position = position;
            DistanceToWall = distanceToWall;
            AoiIndex = aOiIndex;
        }

    }

}