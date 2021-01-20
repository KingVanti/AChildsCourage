using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    internal readonly struct Poi
    {

        internal static Poi ToPoi(Vector2 position) =>
            new Poi(position);


        internal Vector2 Position { get; }


        private Poi(Vector2 position) =>
            Position = position;

    }

}