namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public readonly struct SerializedGroundTile
    {

        public TilePosition Position { get; }


        public SerializedGroundTile(TilePosition position) => Position = position;

    }

}