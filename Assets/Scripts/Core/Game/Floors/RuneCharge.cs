using UnityEngine;
using static AChildsCourage.CustomMath;

namespace AChildsCourage.Game.Floors
{

    public readonly struct RuneCharge
    {

        private const float MinCharge = 0;
        private const float MaxCharge = 1;

        public static RuneCharge NoCharge => new RuneCharge(MinCharge);


        public static RuneCharge Change(float delta, RuneCharge charge) =>
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