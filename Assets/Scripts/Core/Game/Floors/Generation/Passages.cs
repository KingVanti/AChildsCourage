using System;

namespace AChildsCourage.Game.Floors.Generation
{

    [Flags]
    public enum Passages
    {
        North = 1,
        East = 2,
        South = 4,
        West = 8
    };

}