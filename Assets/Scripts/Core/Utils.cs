using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage
{
    public static class Utils
    {

        #region Methods

        /// <summary>
        /// Re-maps a number from one range to another.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="begin1"></param>
        /// <param name="end1"></param>
        /// <param name="begin2"></param>
        /// <param name="end2"></param>
        /// <returns></returns>
        public static float Map(float x, float begin1, float end1, float begin2, float end2)
        {

            float value = begin2 + (end2 - begin2) * ((x - begin1) / (end1 - begin1));
            return value;

        }

        public static T GetWeightedRandom<T>(IEnumerable<T> collection, Func<T, float> weightFunction, IRNG rng)
        {
            var weightedCollection = collection.Select(o => new Weighted<T>(o, weightFunction(o)));

            float totalWeight = weightedCollection.Sum(o => o.Weight);
            float itemWeightIndex = rng.GetValueUnder(totalWeight);
            float currentWeightIndex = 0;

            foreach (var weighted in weightedCollection)
            {
                currentWeightIndex += weighted.Weight;

                if (currentWeightIndex >= itemWeightIndex)
                    return weighted.Object;
            }

            throw new Exception("No element selected. This should not happen!");
        }

        #endregion

    }
}

