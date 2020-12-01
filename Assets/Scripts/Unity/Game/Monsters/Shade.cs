using System;
using System.Collections;
using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Monsters.Navigation;
using UnityEngine;
using static AChildsCourage.Game.Monsters.Navigation.MInvestigation;
using static AChildsCourage.Game.Monsters.Navigation.MInvestigationHistory;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters
{

    public class Shade : MonoBehaviour
    {

        [SerializeField] private FloorStateKeeper floorStateKeeper;
        [Header("Stats")] [SerializeField] private int touchDamage;

        [SerializeField] private int attackDamage;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float investigationUpdatesPerSecond;

        private IEnumerable<TilePosition> currentTilesInVision;
        private InvestigationHistory investigationHistory = Empty;


        private MonsterState CurrentState => new MonsterState(Position, DateTime.Now, investigationHistory);

        private EntityPosition Position => throw new NotImplementedException();

        private FloorState FloorState => floorStateKeeper.CurrentFloorState;


        public void OnTilesInVisionChanged(IEnumerable<TilePosition> positions)
        {
            currentTilesInVision = positions;
        }

        public void OnHuntStarted()
        {
            CancelInvestigation();
        }

        public void OnHuntEnded()
        {
            StartInvestigation();
        }


        private void StartInvestigation()
        {
            // TODO: Start coroutine
        }

        private void CancelInvestigation()
        {
            // TODO: Stop coroutine
        }

        private IEnumerator Investigate()
        {
            var investigation = StartNew(FloorState, CurrentState, RNG.New());
            var currentTarget = NextTarget(investigation, Position);
            SetPathFinderTarget(currentTarget);

            while (!IsComplete(investigation))
            {
                investigation = Progress(investigation, currentTilesInVision);

                var newTarget = NextTarget(investigation, Position);
                if (!newTarget.Equals(currentTarget))
                    SetPathFinderTarget(newTarget);

                yield return new WaitForSeconds(1f / investigationUpdatesPerSecond);
            }

            var completed = Complete(investigation);
            investigationHistory = investigationHistory.Add(completed);

            StartInvestigation();
        }

        private void SetPathFinderTarget(TilePosition tilePosition)
        {
            throw new NotImplementedException();
        }

    }

}