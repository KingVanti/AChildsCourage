using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage
{

    public static class CollectionExtensions
    {

        #region Methods

        public static IEnumerable<Weighted<T>> AttachWeights<T>(this IEnumerable<T> collection, Func<T, float> weightFunction)
        {
            return collection.Select(o => new Weighted<T>(o, weightFunction(o)));
        }

        public static T GetWeightedRandom<T>(this IEnumerable<T> collection, Func<T, float> weightFunction, IRNG rng)
        {
            var weightedCollection = collection.AttachWeights(weightFunction);

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

        public static T GetRandom<T>(this IEnumerable<T> collection, IRNG rng)
        {
            var index = rng.GetValueUnder(collection.Count());

            return collection.ElementAt(index);
        }

        #endregion

    }

}