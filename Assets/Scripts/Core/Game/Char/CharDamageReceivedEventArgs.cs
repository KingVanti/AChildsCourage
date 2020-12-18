using System;

namespace AChildsCourage.Game.Char
{

    public class CharDamageReceivedEventArgs : EventArgs
    {

        public int ReceivedDamage { get; }
        

        public CharDamageReceivedEventArgs(int receivedDamage) => ReceivedDamage = receivedDamage;

    }

}