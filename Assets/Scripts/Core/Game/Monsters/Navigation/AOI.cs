using System.Collections.Immutable;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct Aoi
    {

        internal AoiIndex Index { get; }

        internal TilePosition Center { get; }

        internal ImmutableArray<Poi> PoIs { get; }


        public Aoi(AoiIndex index, TilePosition center, ImmutableArray<Poi> pois)
        {
            Index = index;
            Center = center;
            PoIs = pois;
        }

        internal Aoi(AoiIndex index, TilePosition center)
        {
            Index = index;
            Center = center;
            PoIs = ImmutableArray<Poi>.Empty;
        }


        public override string ToString() => $"AOI {{ Id: {Index} }}";

    }

}