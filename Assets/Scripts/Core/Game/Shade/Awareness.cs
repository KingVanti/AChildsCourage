namespace AChildsCourage.Game.Shade
{

    public static class MAwareness
    {

        private const float MinAwareness = 0;
        private const float MaxAwareness = 1;

        public static Awareness NoAwareness => new Awareness(MinAwareness);


        public static Awareness ChangeBy(float amount, Awareness awareness) =>
            new Awareness(awareness.Value + amount);


        public readonly struct Awareness
        {

            public float Value { get; }


            public Awareness(float value) =>
                Value = value.Clamp(MinAwareness, MaxAwareness);

        }

    }

}