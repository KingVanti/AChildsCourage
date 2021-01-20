using static AChildsCourage.M;

namespace AChildsCourage.Game.Shade
{

    public readonly struct Awareness
    {

        private const float MinAwareness = 0;
        private const float MaxAwareness = 1;

        internal static Awareness NoAwareness => new Awareness(MinAwareness);


        internal static Awareness ChangeBy(float amount, Awareness awareness) =>
            new Awareness(awareness.value + amount);


        private readonly float value;


        private Awareness(float value) =>
            this.value = value.Map(Clamp, MinAwareness, MaxAwareness);


        public static implicit operator float(Awareness awareness) =>
            awareness.value;

    }

}