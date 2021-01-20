namespace AChildsCourage.Game.Floors
{

    internal readonly struct FloorObject
    {

        internal static FloorObject MoveTo(TilePosition tilePosition, FloorObject floorObject) =>
            new FloorObject(tilePosition,
                            floorObject.Data);


        internal TilePosition Position { get; }

        internal FloorObjectData Data { get; }


        internal FloorObject(TilePosition position, FloorObjectData data)
        {
            Position = position;
            Data = data;
        }

    }

}