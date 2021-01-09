using System;
using System.Collections.Immutable;

namespace AChildsCourage.Game.Shade
{

    public readonly struct Investigation
    {

        public static Investigation Complete => new Investigation(null, ImmutableHashSet<Poi>.Empty);


        public static Investigation StartInvestigation(Aoi aoi) =>
            aoi.Pois
               .Map(pois => new Investigation(null, pois.ToImmutableHashSet()))
               .Map(Progress);

        public static Investigation Progress(Investigation investigation)
        {
            var nextTarget = investigation.Map(IsOutOfPois)
                ? (Poi?) null
                : investigation.remainingPois.GetRandom(Rng.RandomRng());
            var remainingPositions = nextTarget != null
                ? investigation.remainingPois.Remove(nextTarget.Value)
                : investigation.remainingPois;

            return new Investigation(nextTarget, remainingPositions);
        }

        public static bool IsComplete(Investigation investigation) =>
            investigation.Map(IsOutOfPois);

        private static bool IsOutOfPois(Investigation investigation) =>
            investigation.remainingPois.IsEmpty;

        public static Poi GetCurrentTarget(Investigation investigation) =>
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