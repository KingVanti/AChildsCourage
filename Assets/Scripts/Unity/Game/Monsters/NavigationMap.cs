using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace AChildsCourage.Game.Monsters {
    public class NavigationMap : MonoBehaviour {

        [SerializeField] private AstarPath astarPath;
        private NavGraph[] navigationGraph;

        public void ScanLevel() {
            navigationGraph = astarPath.graphs;
            Debug.Log(navigationGraph[0]);
            astarPath.Scan(navigationGraph[0]);
        }

    }
}
