using System;
using System.Collections.Generic;
using System.Linq;
using static AChildsCourage.RNG;

namespace AChildsCourage
{

    public static class CollectionRNG
    {

        public delegate float CalculateWeight<T>(T element);


        public static T GetWeightedRandom<T>(this IEnumerable<T> elements, CalculateWeight<T> calculateWeight, CreateRNG createRng) => GetWeightedRandom(calculateWeight, createRng, elements);


        public static T GetWeightedRandom<T>(CalculateWeight<T> calculateWeight, CreateRNG createRng, IEnumerable<T> elements)
        {
            var elementsArray = elements as T[] ?? elements.ToArray();
            if (!elementsArray.Any())
                return default;

            var weightedElements = elementsArray.AttachWeights(calculateWeight).ToArray();

            var totalWeight = weightedElements.Sum(o => o.Weight);
            var itemWeightIndex = createRng.GetValueUnder(totalWeight);
            var currentWeightIndex = 0f;

            foreach (var weightedElement in weightedElements)
            {
                currentWeightIndex += weightedElement.Weight;

                if (currentWeightIndex >= itemWeightIndex)
                    return weightedElement.Element;
            }

            throw new Exception("No element selected. This should not happen!");
        }

        private static IEnumerable<Weighted<T>> AttachWeights<T>(this IEnumerable<T> elements, CalculateWeight<T> calculateWeight)
        {
            return elements.Select(o => AttachWeight(o, calculateWeight));
        }

        private static Weighted<T> AttachWeight<T>(T element, CalculateWeight<T> calculateWeight) => new Weighted<T>(element, calculateWeight(element));


        public static T GetRandom<T>(this IEnumerable<T> elements, CreateRNG createRng) => GetRandom(createRng, elements);


        public static T GetRandom<T>(CreateRNG createRng, IEnumerable<T> elements)
        {
            if (elements.Count() == 0)
                return default;

            var index = createRng.GetValueUnder(elements.Count());

            return elements.ElementAt(index);
        }


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

    }

}