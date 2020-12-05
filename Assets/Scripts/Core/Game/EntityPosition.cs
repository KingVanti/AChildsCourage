using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    public static class MEntityPosition
    {

        public static TilePosition GetEntityTile(EntityPosition position) => new TilePosition((int) position.X, (int) position.Y);


        public readonly struct EntityPosition
        {

            public float X { get; }

            public float Y { get; }


            public EntityPosition(float x, float y)
            {
                X = x;
                Y = y;
            }

        }

    }

}