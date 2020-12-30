using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct GroundTileData
    {

        public TilePosition Position { get; }
        

        public GroundTileData(TilePosition position)
        {
            Position = position;
        }

    }

}