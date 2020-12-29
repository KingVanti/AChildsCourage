using System;

namespace AChildsCourage.Game.Char
{

    public class CharStaminaChangedEventArgs : EventArgs
    {

        public float Stamina { get; }


        public CharStaminaChangedEventArgs(float stamina) =>
            Stamina = stamina;

    }

}