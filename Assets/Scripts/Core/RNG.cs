using System;

namespace AChildsCourage
{

    public readonly struct Rng
    {

        private const float MinPercent = float.Epsilon;
        private const float HundredPercent = 100f;
        

        internal static Rng RngFromSeed(int seed)
        {
            var random = new Random(seed);

            return new Rng(() => (float) random.NextDouble());
        }

        internal static Rng ConstantRng(float value) => new Rng(() => value);
        
        internal static Rng RandomRng() =>
            RngFromSeed(DateTime.Now.GetHashCode());
        
        private static float NextValue(Rng rng) =>
            rng.source();
        
        internal static float GetValueBetween(float min, float max, Rng rng)
        {
            var diff = max - min;
            var dist = diff * rng.Map(NextValue);
            return min + dist;
        }
        
        internal static int GetValueBetween(int min, int max, Rng rng) =>
            (int) rng.Map(GetValueBetween, (float) min, (float) max);
        
        internal static float GetValueUnder(float max, Rng rng) =>
            rng.Map(NextValue) * max;
        
        internal static int GetValueUnder(int max, Rng rng) =>
            (int) rng.Map(GetValueUnder, (float) max);
        
        internal static bool Prob(float variantProb, Rng rng) =>
            rng.Map(GetValueBetween, MinPercent, HundredPercent) <= variantProb;


        private readonly Func<float> source;

        private Rng(Func<float> rngSource) =>
            source = rngSource;

    }

}