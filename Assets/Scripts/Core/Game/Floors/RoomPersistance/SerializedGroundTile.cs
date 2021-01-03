using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct SerializedGroundTile
    {

        public static SerializedGroundTile ApplyTo(SerializedGroundTile _, TilePosition position) =>
            new SerializedGroundTile(position);

        public TilePosition Position { get; }


        public SerializedGroundTile(TilePosition position) => Position = position;

    }

}