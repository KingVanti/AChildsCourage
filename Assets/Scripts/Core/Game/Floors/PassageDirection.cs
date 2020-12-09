using System;

namespace AChildsCourage.Game.Floors
{

    public static class MPassageDirection
    {

        public enum PassageDirection
        {

            North,
            East,
            South,
            West

        }
        

        internal static Func<PassageDirection, PassageDirection> Invert =>
            passage =>
            {
                switch (passage)
                {
                    case PassageDirection.North:
                        return PassageDirection.South;
                    case PassageDirection.East:
                        return PassageDirection.West;
                    case PassageDirection.South:
                        return PassageDirection.North;
                    case PassageDirection.West:
                        return PassageDirection.East;
                }

                throw new Exception("Invalid direction");
            };

    }

}