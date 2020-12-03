using System.Linq;
using Pathfinding;
using UnityEngine;
using static AChildsCourage.Game.Floors.MFloor;

namespace AChildsCourage.Game.Monsters
{

    public class NavigationMap : MonoBehaviour
    {

        [SerializeField] private AstarPath astarPath;
        [SerializeField] private int scanOverfill;
        private Floor mapFloor;

        private GridGraph GridGraph => astarPath.graphs.First() as GridGraph;


        public void OnFloorRecreated(Floor floor)
        {
            mapFloor = floor;
            Invoke(nameof(FitMapToFloor), 1);
        }

        private void FitMapToFloor()
        {
            ScaleToFit(mapFloor);
            astarPath.Scan(GridGraph);
        }


        private void ScaleToFit(Floor floor)
        {
            var (lowerRight, upperLeft) = GetFloorCorners(floor);

            var width = upperLeft.X - lowerRight.X;
            var depth = upperLeft.Y - lowerRight.Y;
            var center = new Vector3(lowerRight.X + width / 2f, lowerRight.Y + depth / 2f, 0);

            GridGraph.center = center;
            GridGraph.SetDimensions(width + scanOverfill, depth + scanOverfill, 1);
        }

    }

}