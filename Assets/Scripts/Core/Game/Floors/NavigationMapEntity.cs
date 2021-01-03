using System.Linq;
using Pathfinding;
using UnityEngine;
using static AChildsCourage.Game.Floors.Floor;

namespace AChildsCourage.Game.Floors
{

    public class NavigationMapEntity : MonoBehaviour
    {

        [SerializeField] private AstarPath pathfinder;


        private GridGraph GridGraph => pathfinder.graphs.First() as GridGraph;

        private FloorDimensions CurrentDimensions
        {
            set
            {
                GridGraph.center = value.Center;
                GridGraph.SetDimensions(value.Width, value.Height, 1);

                pathfinder.Scan(GridGraph);
            }
        }


        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))]
        public void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs) =>
            this.DoAfter(() => ScaleToFit(eventArgs.Floor), 1);

        private void ScaleToFit(Floor floor) =>
            CurrentDimensions = GetFloorDimensions(floor);

    }

}