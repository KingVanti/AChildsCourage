using System;
using System.Collections;
using System.Collections.Generic;
using AChildsCourage.Game.Monsters.Navigation;
using UnityEngine;

namespace AChildsCourage.Game.Monsters
{

    public class Shade : MonoBehaviour
    {

        [Header("Stats")] [SerializeField] private int touchDamage;

        [SerializeField] private int attackDamage;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float investigationUpdatesPerSecond;

        private IEnumerable<TilePosition> currentTilesInVision;
        private InvestigationHistory investigationHistory = InvestigationHistory.Empty;


        private MonsterState CurrentState => new MonsterState(Position, DateTime.Now, investigationHistory);

        private EntityPosition Position => throw new NotImplementedException();

        private FloorState FloorState => throw new NotImplementedException();


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
            var investigation = Investigation.StartNew(FloorState, CurrentState, RNG.New());
            var currentTarget = Investigation.NextTarget(investigation);
            SetPathFinderTarget(currentTarget);

            while (!Investigation.IsComplete(investigation))
            {
                investigation = Investigation.Progress(investigation, currentTilesInVision);

                var newTarget = Investigation.NextTarget(investigation);
                if (!newTarget.Equals(currentTarget))
                    SetPathFinderTarget(currentTarget);

                yield return new WaitForSeconds(1f / investigationUpdatesPerSecond);
            }

            var completed = Investigation.Complete(investigation);
            investigationHistory = InvestigationHistory.Add(investigationHistory, completed);

            StartInvestigation();
        }

        private void SetPathFinderTarget(TilePosition tilePosition)
        {
            throw new NotImplementedException();
        }

    }

}