using System;
using System.Collections.Generic;
using System.Linq;

using static AChildsCourage.RNG;

namespace AChildsCourage
{

    public static class CollectionExtensions
    {

        #region Methods

        public static IEnumerable<Weighted<T>> AttachWeights<T>(this IEnumerable<T> collection, Func<T, float> weightFunction)
        {
            return collection.Select(o => AttachWeight(o, weightFunction));
        }

        private static Weighted<T> AttachWeight<T>(T element, Func<T, float> weightFunction)
        {
            return new Weighted<T>(element, weightFunction(element));
        }


        public static T GetWeightedRandom<T>(this IEnumerable<T> collection, Func<T, float> weightFunction, RNGSource rng)
        {
            if (collection.Count() == 0)
                return default(T);

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


        public static T GetRandom<T>(this IEnumerable<T> collection, RNGSource rng)
        {
            if (collection.Count() == 0)
                return default(T);

            var index = rng.GetValueUnder(collection.Count());

            return collection.ElementAt(index);
        }

        #endregion

    }

}