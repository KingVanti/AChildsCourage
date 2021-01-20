using System;
using System.Collections.Immutable;
using static AChildsCourage.Rng;

namespace AChildsCourage.Game.Shade
{

    internal readonly struct Investigation
    {

        internal static Investigation CompleteInvestigation => new Investigation(null, ImmutableHashSet<Poi>.Empty);


        internal static Investigation StartInvestigation(Aoi aoi) =>
            aoi.Pois
               .Map(pois => new Investigation(null, pois.ToImmutableHashSet()))
               .Map(Progress);

        internal static Investigation Progress(Investigation investigation)
        {
            var nextTarget = investigation.Map(IsOutOfPois)
                ? (Poi?) null
                : investigation.remainingPois.TryGetRandom(RandomRng(), () => throw new Exception("No Pois remaining!"));
            var remainingPositions = nextTarget != null
                ? investigation.remainingPois.Remove(nextTarget.Value)
                : investigation.remainingPois;

            return new Investigation(nextTarget, remainingPositions);
        }

        internal static bool IsComplete(Investigation investigation) =>
            investigation.Map(IsOutOfPois) && investigation.Map(HasNoTarget);

        private static bool IsOutOfPois(Investigation investigation) =>
            investigation.remainingPois.IsEmpty;

        private static bool HasNoTarget(Investigation investigation) =>
            investigation.currentTarget == null;

        internal static Poi GetCurrentTarget(Investigation investigation) =>
            investigation.currentTarget ?? throw new Exception("This investigation has no target!");


        private readonly Poi? currentTarget;
        private readonly ImmutableHashSet<Poi> remainingPois;


        private Investigation(Poi? currentTarget, ImmutableHashSet<Poi> remainingPois)
        {
            this.currentTarget = currentTarget;
            this.remainingPois = remainingPois;
        }

    }

}