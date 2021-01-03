using static UnityEngine.Mathf;

namespace AChildsCourage.Game
{

    public readonly struct EntityPosition
    {

        public static TilePosition GetEntityTile(EntityPosition position) =>
            new TilePosition(FloorToInt(position.X),
                             FloorToInt(position.Y));


        public float X { get; }

        public float Y { get; }


        public EntityPosition(float x, float y)
        {
            X = x;
            Y = y;
        }

    }

}