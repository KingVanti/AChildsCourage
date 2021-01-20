using System;

namespace AChildsCourage.Game.Char
{

    public class CharStaminaChangedEventArgs : EventArgs
    {

        internal float Stamina { get; }


        internal CharStaminaChangedEventArgs(float stamina) =>
            Stamina = stamina;

    }

}