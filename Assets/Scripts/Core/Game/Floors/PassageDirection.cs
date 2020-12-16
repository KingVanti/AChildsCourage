using System;

namespace AChildsCourage.Game.Floors
{

    public static class MPassageDirection
    {

        internal static Func<PassageDirection, PassageDirection> Invert =>
            passage =>
            {
                switch (passage)
                {
                    case PassageDirection.North: return PassageDirection.South;
                    case PassageDirection.East: return PassageDirection.West;
                    case PassageDirection.South: return PassageDirection.North;
                    case PassageDirection.West: return PassageDirection.East;
                    default: throw new Exception("Invalid direction");
                }
            };

        public enum PassageDirection
        {

            North,
            East,
            South,
            West

        }

    }

}