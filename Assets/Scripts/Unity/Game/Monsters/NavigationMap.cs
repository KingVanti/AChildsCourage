using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace AChildsCourage.Game.Monsters {
    public class NavigationMap : MonoBehaviour {

        [SerializeField] private AstarPath astarPath;
        private NavGraph[] navigationGraph;

        public void ScanLevel() {

            Invoke(nameof(Scan), 1f);

        }

        private void Scan() {
            astarPath.Scan(astarPath.graphs[0]);
        }

    }
}
