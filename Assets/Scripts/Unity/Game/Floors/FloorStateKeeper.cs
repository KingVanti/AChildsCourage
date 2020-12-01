using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Monsters.Navigation;
using Ninject.Extensions.Unity;
using UnityEngine;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors
{


    [UseDI]
    public class FloorStateKeeper : MonoBehaviour
    {

        private readonly Dictionary<AOIIndex, AOIState> aoiStates = new Dictionary<AOIIndex, AOIState>();

        public FloorState CurrentFloorState => GenerateFloorState();


        private IEnumerable<AOIState> CurrentAOIStates => aoiStates.Values;


        public void OnGroundTilePlaced(GroundTile groundTile)
        {
            if (!HasStateForIndex(groundTile.AOIIndex))
                aoiStates.Add(groundTile.AOIIndex, new AOIState(groundTile.AOIIndex));
            
            aoiStates[groundTile.AOIIndex].AddPOI(groundTile.Position);
        }

        private bool HasStateForIndex(AOIIndex index) => aoiStates.ContainsKey(index);


        private FloorState GenerateFloorState() =>
            CurrentAOIStates
                .Select(aoiState => aoiState.ToAOI())
                .ToImmutableArray()
                .Map(array => new FloorState(array));

        private class AOIState
        {

            public AOIIndex Index { get; }

            public TilePosition Center { get; }

            public List<POIState> POIStates { get; } = new List<POIState>();

            private ImmutableArray<POI> POIs =>
                POIStates.Select(poiState => poiState.ToPOI())
                         .ToImmutableArray();


            public AOIState(AOIIndex index) => Index = index;


            public AOI ToAOI() => new AOI(Index, Center, POIs);

            public void AddPOI(TilePosition position) => POIStates.Add(new POIState(position));

        }

        private class POIState
        {

            public TilePosition Position { get; }


            public POIState(TilePosition position) => Position = position;


            public POI ToPOI() => new POI(Position);

        }

    }

}