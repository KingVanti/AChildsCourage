using System;

namespace AChildsCourage
{

    public static class MRng
    {

        public delegate float CreateRng();
        
        
        public static CreateRng FromSeed(int seed)
        {
            var random = new Random(seed);

            return () => (float) random.NextDouble();
        }

        public static CreateRng Always(float value)
        {
            return () => value;
        }

        public static CreateRng Random() => FromSeed(DateTime.Now.GetHashCode());


        private static float Next(this CreateRng source) => source();


        public static float GetValueBetween(this CreateRng source, float min, float max)
        {
            var diff = max - min;
            var dist = diff * source.Next();

            return min + dist;
        }


        public static int GetValueBetween(this CreateRng source, int min, int max) => (int) source.GetValueBetween((float) min, max);


        public static float GetValueUnder(this CreateRng source, float max) => source.Next() * max;


        public static int GetValueUnder(this CreateRng source, int max) => (int) source.GetValueUnder((float) max);


        public static bool Prob(this CreateRng source, float variantProb) => source.GetValueBetween(float.Epsilon, 100f) <= variantProb;

    }

}