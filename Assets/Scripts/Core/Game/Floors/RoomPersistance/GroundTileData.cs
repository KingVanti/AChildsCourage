using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct GroundTileData
    {

        public static GroundTileData ApplyTo(GroundTileData _, TilePosition position) =>
            new GroundTileData(position);

        public TilePosition Position { get; }


        public GroundTileData(TilePosition position) => Position = position;

    }

}