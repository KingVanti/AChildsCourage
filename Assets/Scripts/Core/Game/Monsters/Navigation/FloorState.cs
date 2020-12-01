using System.Collections.Immutable;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct FloorState
    {

        public ImmutableArray<AOI> AOIs { get; }


        public FloorState(ImmutableArray<AOI> aoIs) => AOIs = aoIs;

        public FloorState(params AOI[] aoIs) => AOIs = ImmutableArray.Create(aoIs);

    }

}