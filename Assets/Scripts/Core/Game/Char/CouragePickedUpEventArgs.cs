using System;
using AChildsCourage.Game.Floors.Courage;

namespace AChildsCourage.Game.Char
{

    public class CouragePickedUpEventArgs : EventArgs
    {

        public CourageVariant Variant { get; }


        public CouragePickedUpEventArgs(CourageVariant variant) => 
            Variant = variant;

    }

}