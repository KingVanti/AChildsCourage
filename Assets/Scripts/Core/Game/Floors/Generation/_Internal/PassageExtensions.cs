using System;

namespace AChildsCourage.Game.Floors.Generation
{

    internal static class PassageExtensions
    {

        #region Methods

        internal static Passage Invert(this Passage passage)
        {
            switch (passage)
            {
                case Passage.North:
                    return Passage.South;
                case Passage.East:
                    return Passage.West;
                case Passage.South:
                    return Passage.North;
                case Passage.West:
                    return Passage.East;
            }

            throw new Exception("Invalid direction");
        }

        #endregion

    }

}