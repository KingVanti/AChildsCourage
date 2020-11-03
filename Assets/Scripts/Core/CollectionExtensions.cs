using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage
{

    public static class CollectionExtensions 
    {

        #region Methods

        public static T GetWeightedRandom<T>(this IEnumerable<T> collection, Func<T, float> weightFunction, IRNG rng)
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