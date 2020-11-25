using System;
using System.Collections.Generic;
using System.Linq;
using static AChildsCourage.RNG;

namespace AChildsCourage
{

    public static class CollectionRNG
    {

        public delegate float WeightFunction<T>(T element);


        private class Weighted<T>
        {

            internal T Element { get; }

            internal float Weight { get; }


            internal Weighted(T element, float weight)
            {
                Element = element;
                Weight = weight;
            }

        }


        public static T GetWeightedRandom<T>(this IEnumerable<T> elements, WeightFunction<T> weightFunction, RNGSource rng)
        {
            return GetWeightedRandom(weightFunction, rng, elements);
        }


        public static T GetWeightedRandom<T>(WeightFunction<T> weightFunction, RNGSource rng, IEnumerable<T> elements)
        {
            if (elements.Count() == 0)
                return default(T);

            var weightedElements = elements.AttachWeights(weightFunction);

            var totalWeight = weightedElements.Sum(o => o.Weight);
            var itemWeightIndex = rng.GetValueUnder(totalWeight);
            var currentWeightIndex = 0f;

            foreach (var weightedElement in weightedElements)
            {
                currentWeightIndex += weightedElement.Weight;

                if (currentWeightIndex >= itemWeightIndex)
                    return weightedElement.Element;
            }

            throw new Exception("No element selected. This should not happen!");
        }

        private static IEnumerable<Weighted<T>> AttachWeights<T>(this IEnumerable<T> elements, WeightFunction<T> weightFunction)
        {
            return elements.Select(o => AttachWeight(o, weightFunction));
        }

        private static Weighted<T> AttachWeight<T>(T element, WeightFunction<T> weightFunction)
        {
            return new Weighted<T>(element, weightFunction(element));
        }


        public static T GetRandom<T>(this IEnumerable<T> elements, RNGSource rng)
        {
            return GetRandom(rng, elements);
        }


        public static T GetRandom<T>(RNGSource rng, IEnumerable<T> elements)
        {
            if (elements.Count() == 0)
                return default(T);

            var index = rng.GetValueUnder(elements.Count());

            return elements.ElementAt(index);
        }

    }

}