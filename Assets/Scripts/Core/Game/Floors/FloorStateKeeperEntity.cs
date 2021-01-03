using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Shade.Navigation;
using UnityEngine;
using static AChildsCourage.Game.TilePosition;

namespace AChildsCourage.Game.Floors
{

    public class FloorStateKeeperEntity : MonoBehaviour
    {

        private readonly Dictionary<AoiIndex, AoiState> aoiStates = new Dictionary<AoiIndex, AoiState>();
        private FloorState currentFloorState;
        private bool outDatedFloorState;
        private readonly Dictionary<ChunkPosition, AoiIndex> registeredAoiIndices = new Dictionary<ChunkPosition, AoiIndex>();
        private AoiIndex currentAoiIndex;


        public FloorState CurrentFloorState
        {
            get
            {
                if (outDatedFloorState) UpdateFloorState();

                return currentFloorState;
            }
        }

        private IEnumerable<AoiState> CurrentAoiStates => aoiStates.Values;


        public void OnGroundTilePlaced(TilePosition groundPosition)
        {
            var aoiIndex = GetAoiIndexFor(groundPosition);
            if (!HasStateForIndex(aoiIndex)) AddAoiFor(aoiIndex);

            AddPoiFor(groundPosition, aoiIndex);
            outDatedFloorState = true;
        }

        private AoiIndex GetAoiIndexFor(TilePosition groundPosition)
        {
            var chunk = groundPosition.Map(GetChunk);

            if (!registeredAoiIndices.ContainsKey(chunk)) registeredAoiIndices.Add(chunk, currentAoiIndex++);

            return registeredAoiIndices[chunk];
        }

        private bool HasStateForIndex(AoiIndex index) =>
            aoiStates.ContainsKey(index);

        private void AddAoiFor(AoiIndex aoiIndex) =>
            aoiStates.Add(aoiIndex, new AoiState(aoiIndex));

        private void AddPoiFor(TilePosition groundTile, AoiIndex aoiIndex) =>
            aoiStates[aoiIndex].AddPoi(groundTile);

        private void UpdateFloorState()
        {
            currentFloorState = GenerateFloorState();
            outDatedFloorState = false;
        }

        private FloorState GenerateFloorState() =>
            CurrentAoiStates
                .Select(aoiState => aoiState.ToAoi())
                .ToImmutableArray()
                .Map(array => new FloorState(array));


        private class AoiState
        {

            private AoiIndex Index { get; }

            private List<PoiState> PoiStates { get; } = new List<PoiState>();

            private ImmutableArray<Poi> Pois =>
                PoiStates.Select(poiState => poiState.ToPoi())
                         .ToImmutableArray();

            public AoiState(AoiIndex index) => Index = index;


            public Aoi ToAoi() => new Aoi(Index, CalculateCenter(), Pois);

            private TilePosition CalculateCenter() =>
                Pois.Select(p => p.Position)
                    .ToImmutableHashSet()
                    .Map(Average);

            public void AddPoi(TilePosition position) =>
                PoiStates.Add(new PoiState(position));

        }

        private class PoiState
        {

            private TilePosition Position { get; }


            public PoiState(TilePosition position) => Position = position;


            public Poi ToPoi() =>
                new Poi(Position);

        }

    }

}