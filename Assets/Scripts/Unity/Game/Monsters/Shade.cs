using System;
using System.Collections;
using System.Collections.Generic;
using AChildsCourage.Game.Monsters.Navigation;
using UnityEngine;
using static AChildsCourage.Game.Monsters.Navigation.InvestigationInProgress;

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


        private MonsterState CurrentState => MonsterState.Create(Position, investigationHistory);

        private EntityPosition Position => throw new NotImplementedException();

        private FloorAOIs FloorAOIs => throw new NotImplementedException();


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
            var investigation = StartNew(FloorAOIs, CurrentState);
            var currentTarget = investigation.TargetPosition;
            SetPathFinderTarget(currentTarget);

            while (!IsComplete(investigation))
            {
                investigation = Progress(investigation, currentTilesInVision);

                var newTarget = investigation.TargetPosition;
                if (!newTarget.Equals(currentTarget))
                    SetPathFinderTarget(currentTarget);

                yield return new WaitForSeconds(1f / investigationUpdatesPerSecond);
            }

            var completed = Complete(investigation);
            investigationHistory = InvestigationHistory.Add(investigationHistory, completed);

            StartInvestigation();
        }

        private void SetPathFinderTarget(TilePosition tilePosition)
        {
            throw new NotImplementedException();
        }

    }

}