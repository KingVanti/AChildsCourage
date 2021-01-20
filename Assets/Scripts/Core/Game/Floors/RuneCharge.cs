using UnityEngine;
using static AChildsCourage.M;

namespace AChildsCourage.Game.Floors
{

    public readonly struct RuneCharge
    {

        private const float MinCharge = 0;
        private const float MaxCharge = 1;

        internal static RuneCharge NoCharge => new RuneCharge(MinCharge);


        internal static RuneCharge Change(float delta, RuneCharge charge) =>
            charge.value
                  .Map(Plus, delta)
                  .Map(Clamp, MinCharge, MaxCharge)
                  .Map(c => new RuneCharge(c));


        private readonly float value;


        private RuneCharge(float value) =>
            this.value = value;


        public override string ToString() =>
            $"Charge: {Mathf.RoundToInt(value * 100)}%";


        public static implicit operator float(RuneCharge charge) =>
            charge.value;

    }

}