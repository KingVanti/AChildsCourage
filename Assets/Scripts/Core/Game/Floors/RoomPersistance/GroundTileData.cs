namespace AChildsCourage.Game.Floors.RoomPersistance
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