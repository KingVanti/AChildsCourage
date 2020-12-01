namespace AChildsCourage.Game
{

    public readonly struct EntityPosition
    {

        public float X { get; }

        public float Y { get; }


        public EntityPosition(float x, float y)
        {
            X = x;
            Y = y;
        }


        public static TilePosition GetTilePosition(EntityPosition position) => new TilePosition((int) position.X, (int) position.Y);

    }

}