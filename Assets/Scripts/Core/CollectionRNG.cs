using System;
using System.Collections.Generic;
using System.Linq;
using static AChildsCourage.Rng;

namespace AChildsCourage
{

    internal static class CollectionRng
    {

        internal static T TryGetWeightedRandom<T>(this IEnumerable<T> elements, CalculateWeight<T> calculateWeight, Rng rng, Func<T> onEmpty) =>
            elements.Select(AttachWeight, calculateWeight)
                    .ToArray()
                    .Map(TryGetWeightedRandom, rng, onEmpty);

        private static T TryGetWeightedRandom<T>(Rng rng, Func<T> onEmpty, IReadOnlyList<Weighted<T>> weightedElements) =>
            weightedElements.Any()
                ? weightedElements.Map(GetWeightedRandom, rng)
                : onEmpty();

        private static T GetWeightedRandom<T>(Rng rng, IReadOnlyList<Weighted<T>> weightedElements)
        {
            var itemWeightIndex = weightedElements.Map(CalculateItemWeightIndex, rng);
            var currentWeightIndex = 0f;

            foreach (var weightedElement in weightedElements)
            {
                currentWeightIndex += weightedElement.Weight;

                if (currentWeightIndex >= itemWeightIndex) return weightedElement.Element;
            }

            throw new Exception("No element selected. This should not happen!");
        }

        private static float CalculateItemWeightIndex<T>(Rng rng, IEnumerable<Weighted<T>> weightedElements)
        {
            var totalWeight = weightedElements.Map(CalculateTotalWeight);
            return rng.Map(GetValueUnder, totalWeight);
        }

        private static float CalculateTotalWeight<T>(IEnumerable<Weighted<T>> weightedElements) =>
            weightedElements.Sum(o => o.Weight);

        private static Weighted<T> AttachWeight<T>(CalculateWeight<T> calculateWeight, T element) =>
            new Weighted<T>(element, calculateWeight(element));

        internal static T TryGetRandom<T>(this IEnumerable<T> elements, Rng rng, Func<T> onEmpty) =>
            elements.ToArray()
                    .Map(TryGetRandom, rng, onEmpty);

        private static T TryGetRandom<T>(Rng rng, Func<T> onEmpty, IReadOnlyList<T> elements) =>
            elements.Any()
                ? elements.Map(GetRandom, rng)
                : onEmpty();

        private static T GetRandom<T>(Rng rng, IReadOnlyList<T> elements) =>
            elements[elements.Map(ChooseRandomIndex, rng)];

        private static int ChooseRandomIndex<T>(Rng rng, IReadOnlyCollection<T> elements) =>
            rng.Map(GetValueUnder, elements.Count);


        internal delegate float CalculateWeight<in T>(T element);


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