using System;

namespace AChildsCourage
{

    public static class RNG
    {

        public delegate RNGSource RNGInitializer(int seed);

        public delegate float RNGSource();


        public static RNGInitializer SeedBasedRNG { get; } = FromSeed;

        public static RNGSource FromSeed(int seed)
        {
            var random = new Random(seed);

            return () => (float) random.NextDouble();
        }


        public static float GetValue01(this RNGSource source)
        {
            return source();
        }


        public static float GetValueBetween(this RNGSource source, float min, float max)
        {
            var diff = max - min;
            var dist = diff * GetValue01(source);

            return min + dist;
        }


        public static int GetValueBetween(this RNGSource source, int min, int max)
        {
            return (int) GetValueBetween(source, (float) min, max);
        }


        public static float GetValueUnder(this RNGSource source, float max)
        {
            return GetValue01(source) * max;
        }


        public static int GetValueUnder(this RNGSource source, int max)
        {
            return (int) GetValueUnder(source, (float) max);
        }


        public static bool Prob(this RNGSource source, float variantProb)
        {
            return GetValueBetween(source, float.Epsilon, 100f) <= variantProb;
        }

    }

}