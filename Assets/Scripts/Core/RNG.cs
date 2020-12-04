using System;

namespace AChildsCourage
{

    public static class Rng
    {

        public delegate float CreateRng();

        public delegate CreateRng InitializeRngSource(int seed);


        public static InitializeRngSource SeedBasedInitializeRng { get; } = FromSeed;

        public static CreateRng FromSeed(int seed)
        {
            var random = new Random(seed);

            return () => (float) random.NextDouble();
        }

        public static CreateRng Always(float value)
        {
            return () => value;
        }

        public static CreateRng New() => FromSeed(DateTime.Now.GetHashCode());


        public static float GetValue01(this CreateRng source) => source();


        public static float GetValueBetween(this CreateRng source, float min, float max)
        {
            var diff = max - min;
            var dist = diff * GetValue01(source);

            return min + dist;
        }


        public static int GetValueBetween(this CreateRng source, int min, int max) => (int) GetValueBetween(source, (float) min, max);


        public static float GetValueUnder(this CreateRng source, float max) => GetValue01(source) * max;


        public static int GetValueUnder(this CreateRng source, int max) => (int) GetValueUnder(source, (float) max);


        public static bool Prob(this CreateRng source, float variantProb) => GetValueBetween(source, float.Epsilon, 100f) <= variantProb;

    }

}