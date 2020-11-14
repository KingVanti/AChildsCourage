using System;

namespace AChildsCourage
{

    internal class RNG : IRNG
    {

        #region Fields

        private Random rng;

        #endregion

        #region Constructors

        public RNG(int seed)
        {
            rng = new Random(seed);
        }

        #endregion

        #region Methods

        public float GetValue01()
        {
            return rng.Next(1);
        }

        public float GetValueBetween(float min, float max)
        {
            return (float)(rng.NextDouble() * (max - min) + min);
        }

        public int GetValueBetween(int min, int max)
        {
            return rng.Next(min, max);
        }

        public float GetValueUnder(float max)
        {
            return (float)(rng.NextDouble() * (max - 0) + 0);
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
