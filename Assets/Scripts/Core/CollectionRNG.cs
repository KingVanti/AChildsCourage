using System;
using System.Collections.Generic;
using System.Linq;
using static AChildsCourage.Rng;

namespace AChildsCourage
{

    internal static class CollectionRng
    {

        internal delegate float CalculateWeight<in T>(T element);


        internal static T GetWeightedRandom<T>(this IEnumerable<T> elements, CalculateWeight<T> calculateWeight, Rng rng) =>
            GetWeightedRandom(calculateWeight, rng, elements);


        private static T GetWeightedRandom<T>(CalculateWeight<T> calculateWeight, Rng rng, IEnumerable<T> elements)
        {
            var weightedElements = elements.AttachWeights(calculateWeight).ToArray();

            if (!weightedElements.Any()) return default;

            var totalWeight = weightedElements.Sum(o => o.Weight);
            var itemWeightIndex = rng.Map(GetValueUnder, totalWeight);
            var currentWeightIndex = 0f;

            foreach (var weightedElement in weightedElements)
            {
                currentWeightIndex += weightedElement.Weight;

                if (currentWeightIndex >= itemWeightIndex) return weightedElement.Element;
            }

            throw new Exception("No element selected. This should not happen!");
        }

        private static IEnumerable<Weighted<T>> AttachWeights<T>(this IEnumerable<T> elements, CalculateWeight<T> calculateWeight) =>
            elements.Select(o => AttachWeight(o, calculateWeight));


        private static Weighted<T> AttachWeight<T>(T element, CalculateWeight<T> calculateWeight) =>
            new Weighted<T>(element, calculateWeight(element));


        internal static T GetRandom<T>(this IEnumerable<T> elements, Rng rng) =>
            GetRandom(rng, elements);


        private static T GetRandom<T>(Rng rng, IEnumerable<T> elements)
        {
            var elementsArray = elements.ToArray();

            if (!elementsArray.Any()) return default;

            var index = rng.Map(GetValueUnder, elementsArray.Length);
            return elementsArray[index];
        }


        private readonly struct Weighted<T>
        {

            internal T Element { get; }

            internal float Weight { get; }


            internal Weighted(T element, float weight)
            {
                Element = element;
                Weight = weight;
            }

        }

    }

}