using System;
using AChildsCourage.Game.Floors.Courage;

namespace AChildsCourage.Game.Char
{

    public class CouragePickedUpEventArgs : EventArgs
    {

        internal CourageVariant Variant { get; }


        internal CouragePickedUpEventArgs(CourageVariant variant) =>
            Variant = variant;

    }

}