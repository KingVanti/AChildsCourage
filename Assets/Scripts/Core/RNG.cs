using System;

namespace AChildsCourage
{

    internal class RNG : IRNG
    {

        #region Fields

        private readonly Random rng;

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
            return (int)GetValueBetween((float)min, (float)max);
        }


        public float GetValueUnder(float max)
        {
            return GetValue01() * max;
        }


        public int GetValueUnder(int max)
        {
            return (int)GetValueUnder((float)max);
        }


        public bool Prob(float variantProb)
        {
            return GetValueBetween(float.Epsilon, 100f) <= variantProb;
        }

        #endregion

    }

}
