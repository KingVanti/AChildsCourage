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

        private Coroutine investigationCoroutine;


        private MonsterState CurrentState => new MonsterState(Position, DateTime.Now, investigationHistory);

        private EntityPosition Position => new EntityPosition(transform.position.x, transform.position.y);

        private FloorState FloorState => floorStateKeeper.CurrentFloorState;

        private void Awake() {
            Invoke(nameof(StartInvestigation), 1f);
        }

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
            investigationCoroutine = StartCoroutine(Investigate());
        }

        private void CancelInvestigation()
        {
            StopCoroutine(investigationCoroutine);
            investigationCoroutine = null;
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
                if (!newTarget.Equals(currentTarget)) {
                    SetPathFinderTarget(newTarget);
                    currentTarget = newTarget;
                }

                yield return new WaitForSeconds(1f / investigationUpdatesPerSecond);
            }

            var completed = Complete(investigation);
            investigationHistory = investigationHistory.Add(completed);

            StartInvestigation();
        }

        private void SetPathFinderTarget(TilePosition tilePosition)
        {
            Vector3 newPos = tilePosition.ToVector3() + new Vector3(0.5f, 0.5f, 0);
            Vector3 deltaPos = newPos - transform.position;
            transform.GetChild(0).right = deltaPos;
            transform.position = newPos;
        }

        private void OnDestroy() {
            CancelInvestigation();
        }

    }

}