using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct GroundTileData
    {

        public TilePosition Position { get; }

        public int DistanceToWall { get; }

        public int AoiIndex { get; }


        public GroundTileData(TilePosition position, int distanceToWall, int aoiIndex)
        {
            Position = position;
            DistanceToWall = distanceToWall;
            AoiIndex = aoiIndex;
        }

    }

}