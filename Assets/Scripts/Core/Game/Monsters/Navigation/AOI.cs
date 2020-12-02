using System.Collections.Immutable;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct AOI
    {

        internal AOIIndex Index { get; }

        internal TilePosition Center { get; }

        internal ImmutableArray<POI> POIs { get; }


        public AOI(AOIIndex index, TilePosition center, ImmutableArray<POI> pois)
        {
            Index = index;
            Center = center;
            POIs = pois;
        }

        internal AOI(AOIIndex index, TilePosition center)
        {
            Index = index;
            Center = center;
            POIs = ImmutableArray<POI>.Empty;
        }


        public override string ToString() => $"AOI {{ Id: {Index} }}";

    }

}