using static AChildsCourage.CustomMath;

namespace AChildsCourage.Game.Shade
{

    public readonly struct Awareness
    {

        private const float MinAwareness = 0;
        private const float MaxAwareness = 1;

        public static Awareness NoAwareness => new Awareness(MinAwareness);

        public static Awareness TotalAwareness = new Awareness(MaxAwareness);


        public static Awareness ChangeBy(float amount, Awareness awareness) =>
            new Awareness(awareness.value + amount);


        private readonly float value;


        public Awareness(float value) =>
            this.value = value.Map(Clamp, MinAwareness, MaxAwareness);


        public static implicit operator float(Awareness awareness) =>
            awareness.value;

    }

}