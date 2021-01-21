using System.Linq;
using JetBrains.Annotations;
using Pathfinding;
using UnityEngine;
using static AChildsCourage.Game.Floors.Floor;
using static AChildsCourage.Game.IntBounds;

namespace AChildsCourage.Game.Floors
{

    public class NavigationMapEntity : MonoBehaviour
    {

        [SerializeField] private AstarPath pathfinder;


        private GridGraph GridGraph => pathfinder.graphs.First() as GridGraph;

        private IntBounds CurrentBounds
        {
            set
            {
                GridGraph.center = value.Map(Center);
                GridGraph.SetDimensions(value.Map(Width), value.Map(Height), 1);

                pathfinder.Scan(GridGraph);
            }
        }


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))] [UsedImplicitly]
        public void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs) =>
            this.DoAfter(() => ScaleToFit(eventArgs.Floor), 1);

        private void ScaleToFit(Floor floor) =>
            CurrentBounds = GetFloorBounds(floor);

    }

}