using System;
using AChildsCourage.Game.Floors.Courage;

namespace AChildsCourage.Game.Char
{

    public class CouragePickedUpEventArgs : EventArgs
    {

        public int Value { get; }

        public CourageVariant Variant { get; }


        public CouragePickedUpEventArgs(int value, CourageVariant variant)
        {
            Value = value;
            Variant = variant;
        }

    }

}