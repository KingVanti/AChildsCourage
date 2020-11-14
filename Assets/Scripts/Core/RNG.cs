using System;

namespace AChildsCourage
{

    internal class RNG : IRNG
    {

        #region Fields

        private readonly Random rng;

        #endregion

        #region Constructors

        internal RNG(int seed)
        {
            rng = new Random(seed);
        }

        #endregion

        #region Methods

        public float GetValue01()
        {
            return (float)rng.NextDouble();
        }


        public float GetValueBetween(float min, float max)
        {
            var diff = max - min;
            var dist = diff * GetValue01();

            return min + dist;
        }


        public int GetValueBetween(int min, int max)
        {
            return rng.Next(min, max);
        }


        public float GetValueUnder(float max)
        {
            return GetValue01() * max;
        }


        public int GetValueUnder(int max)
        {
            return rng.Next(max);
        }


        public bool Prob(float variantProb)
        {
            return GetValueBetween(float.Epsilon, 100f) <= variantProb;
        }

        #endregion

    }

}
