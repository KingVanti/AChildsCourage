using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public readonly struct Aoi
    {

        public static Aoi ToAoi(IEnumerable<Vector2> positions) =>
            new Aoi(positions.Select(p => p.Map(Poi.ToPoi)).ToImmutableHashSet());


        public ImmutableHashSet<Poi> Pois { get; }


        private Aoi(ImmutableHashSet<Poi> pois) =>
            Pois = pois;

    }

}