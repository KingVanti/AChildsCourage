using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Monsters.Navigation;
using Ninject.Extensions.Unity;
using Pathfinding;
using UnityEngine;
using UnityEngine.Events;
using static AChildsCourage.Game.Monsters.Navigation.MInvestigation;
using static AChildsCourage.Game.Monsters.Navigation.MInvestigationHistory;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters
{

    [UseDI]
    public class Shade : MonoBehaviour {
        #region Fields

        public AIPath ai;

#pragma warning disable 649
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Seeker seeker;
        [Header("Stats")] [SerializeField] private int touchDamage;
        [SerializeField] private int attackDamage;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float investigationUpdatesPerSecond;
        [SerializeField] private Transform targetTransform;
        [Range(1, 50)][SerializeField] private float minimumDistanceTargetLock;
#pragma warning restore 649

        private IEnumerable<TilePosition> currentTilesInVision = Enumerable.Empty<TilePosition>();
        private InvestigationHistory investigationHistory = Empty;

        private Coroutine investigationCoroutine;

        [Header("Events")]
        public Vector3Event OnMinimumDistanceEntered;
        public UnityEvent OnMinimumDistanceLeft;
        

        #endregion

        #region Properties

        [AutoInject] public FloorStateKeeper FloorStateKeeper { private get; set; }

        private MonsterState CurrentState => new MonsterState(Position, DateTime.Now, investigationHistory);

        private EntityPosition Position => new EntityPosition(transform.position.x, transform.position.y);

        private FloorState FloorState => FloorStateKeeper.CurrentFloorState;

        #endregion

        #region Methods

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

        public void StartInvestigation()
        {
            investigationCoroutine = StartCoroutine(Investigate());
        }

        
        private void CancelInvestigation()
        {
            if (investigationCoroutine != null)
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

                if(Mathf.Abs(Vector2.Distance(ai.target.position, transform.position)) < minimumDistanceTargetLock) {
                    OnMinimumDistanceEntered?.Invoke(ai.target.position);
                } else {
                    OnMinimumDistanceLeft?.Invoke();
                }


                yield return new WaitForSeconds(1f / investigationUpdatesPerSecond);
            }

            var completed = Complete(investigation);
            investigationHistory = investigationHistory.Add(completed);

            StartInvestigation();
        }


        private void SetPathFinderTarget(TilePosition tilePosition)
        {

            targetTransform.position = tilePosition.ToVector3() + new Vector3(0.5f, 0.5f, 0);
            ai.target = targetTransform;

        }


        private void OnDestroy() {
            CancelInvestigation();
        }

        #endregion

        #region Subclasses

        [Serializable]
        public class Vector3Event : UnityEvent<Vector3> { }

        #endregion

    }

}