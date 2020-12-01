using System;

namespace AChildsCourage
{

    public static class RNG
    {

        public delegate float CreateRNG();

        public delegate CreateRNG InitializeRNGSource(int seed);


        public static InitializeRNGSource SeedBasedInitializeRng { get; } = FromSeed;

        public static CreateRNG FromSeed(int seed)
        {
            var random = new Random(seed);

            return () => (float) random.NextDouble();
        }

        public static CreateRNG Always(float value)
        {
            return () => value;
        }

        public static CreateRNG New() => FromSeed(DateTime.Now.GetHashCode());


        public static float GetValue01(this CreateRNG source) => source();


        public static float GetValueBetween(this CreateRNG source, float min, float max)
        {
            var diff = max - min;
            var dist = diff * GetValue01(source);

            return min + dist;
        }


        public static int GetValueBetween(this CreateRNG source, int min, int max) => (int) GetValueBetween(source, (float) min, max);


        public static float GetValueUnder(this CreateRNG source, float max) => GetValue01(source) * max;


        public static int GetValueUnder(this CreateRNG source, int max) => (int) GetValueUnder(source, (float) max);


        public static bool Prob(this CreateRNG source, float variantProb) => GetValueBetween(source, float.Epsilon, 100f) <= variantProb;

    }

}