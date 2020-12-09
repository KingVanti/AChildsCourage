using System.Collections.Immutable;

namespace AChildsCourage.Game.Shade.Navigation
{

    public readonly struct FloorState
    {

        public ImmutableArray<Aoi> AOIs { get; }


        public FloorState(ImmutableArray<Aoi> aoIs) => AOIs = aoIs;

        public FloorState(params Aoi[] aoIs) => AOIs = ImmutableArray.Create(aoIs);

    }

}