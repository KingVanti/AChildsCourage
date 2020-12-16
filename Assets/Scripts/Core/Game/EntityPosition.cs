using System;
using static AChildsCourage.Game.MTilePosition;
using static UnityEngine.Mathf;

namespace AChildsCourage.Game
{

    public static class MEntityPosition
    {

        public static Func<EntityPosition, TilePosition> GetEntityTile =>
            position =>
                new TilePosition(FloorToInt(position.X),
                                 FloorToInt(position.Y));


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