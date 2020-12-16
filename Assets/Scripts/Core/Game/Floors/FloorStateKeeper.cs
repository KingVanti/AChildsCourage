using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Shade.Navigation;
using Ninject.Extensions.Unity;
using UnityEngine;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{

    [UseDi]
    public class FloorStateKeeper : MonoBehaviour
    {

        private readonly Dictionary<AoiIndex, AoiState> aoiStates = new Dictionary<AoiIndex, AoiState>();
        private FloorState lastFloorState;
        private bool outDatedFloorState;

        public FloorState CurrentFloorState
        {
            get
            {
                if (outDatedFloorState)
                {
                    lastFloorState = GenerateFloorState();
                    outDatedFloorState = false;
                }

                return lastFloorState;
            }
        }

        private IEnumerable<AoiState> CurrentAoiStates => aoiStates.Values;


        public void OnGroundTilePlaced(GroundTile groundTile)
        {
            if (!HasStateForIndex(groundTile.AoiIndex)) aoiStates.Add(groundTile.AoiIndex, new AoiState(groundTile.AoiIndex));

            aoiStates[groundTile.AoiIndex].AddPoi(groundTile.Position);
            outDatedFloorState = true;
        }

        private bool HasStateForIndex(AoiIndex index) => aoiStates.ContainsKey(index);


        private FloorState GenerateFloorState() =>
            CurrentAoiStates
                .Select(aoiState => aoiState.ToAoi())
                .ToImmutableArray()
                .Map(array => new FloorState(array));

        private class AoiState
        {

            public AoiIndex Index { get; }

            public List<PoiState> PoiStates { get; } = new List<PoiState>();

            private ImmutableArray<Poi> Pois =>
                PoiStates.Select(poiState => poiState.ToPoi())
                         .ToImmutableArray();


            public AoiState(AoiIndex index) => Index = index;


            public Aoi ToAoi() => new Aoi(Index, CalculateCenter(), Pois);

            private TilePosition CalculateCenter()
            {
                var positions = Pois.Select(p => p.Position).ToImmutableHashSet();

                return new TilePosition(
                                        (int) positions.Select(p => p.X).Average(),
                                        (int) positions.Select(p => p.Y).Average());
            }

            public void AddPoi(TilePosition position) => PoiStates.Add(new PoiState(position));

        }

        private class PoiState
        {

            public TilePosition Position { get; }


            public PoiState(TilePosition position) => Position = position;


            public Poi ToPoi() => new Poi(Position);

        }

    }

}