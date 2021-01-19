namespace AChildsCourage.Game.Char
{

    public readonly struct Tension
    {

        private const float MinTension = 0;
        private const float MaxTension = 1;


        public static Tension NoTension => new Tension(MinTension);


        private readonly float value;


        private Tension(float value) =>
            this.value = value.Clamp(MinTension, MaxTension);


        public static implicit operator Tension(float value) =>
            new Tension(value);

        public static implicit operator float(Tension tension) =>
            tension.value;

    }

}