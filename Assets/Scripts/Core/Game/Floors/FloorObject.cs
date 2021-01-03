namespace AChildsCourage.Game.Floors
{

    public readonly struct FloorObject
    {

        public static FloorObject MoveTo(TilePosition tilePosition, FloorObject floorObject) =>
            new FloorObject(tilePosition,
                            floorObject.Data);


        public TilePosition Position { get; }

        public FloorObjectData Data { get; }


        public FloorObject(TilePosition position, FloorObjectData data)
        {
            Position = position;
            Data = data;
        }

    }

}