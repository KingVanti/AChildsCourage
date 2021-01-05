using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public readonly struct Poi
    {

        public static Poi ToPoi(Vector2 position) =>
            new Poi(position);


        public Vector2 Position { get; }


        private Poi(Vector2 position) =>
            Position = position;

    }

}