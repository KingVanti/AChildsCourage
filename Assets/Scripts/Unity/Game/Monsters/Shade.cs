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
using static AChildsCourage.Game.MEntityPosition;
using static AChildsCourage.Game.Monsters.MAwareness;
using static AChildsCourage.Game.Monsters.Navigation.MInvestigation;
using static AChildsCourage.Game.Monsters.Navigation.MInvestigationHistory;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters
{

    [UseDi]
    public class Shade : MonoBehaviour
    {

        #region Subclasses

        [Serializable]
        public class Vector3Event : UnityEvent<Vector3> { }

        #endregion

        #region Static Fields

        private static readonly int MovingAnimatorKey = Animator.StringToHash("IsMoving");
        private static readonly int XAnimatorKey = Animator.StringToHash("X");
        private static readonly int YAnimatorKey = Animator.StringToHash("Y");

        #endregion

        #region Fields

        public AIPath ai;
        public Vector3Event OnMinimumDistanceEntered;
        public UnityEvent OnMinimumDistanceLeft;

#pragma warning disable 649
        [SerializeField] private Animator shadeAnimator;
        [SerializeField] private float investigationUpdatesPerSecond;
        [SerializeField] private int touchDamage;
        [Range(1, 50)] [SerializeField] private float minimumDistanceTargetLock;
#pragma warning restore 649

        private IEnumerable<TilePosition> currentTilesInVision = Enumerable.Empty<TilePosition>();
        private InvestigationHistory investigationHistory = Empty;
        private Coroutine investigationCoroutine;

        #endregion

        #region Properties

        [AutoInject] public FloorStateKeeper FloorStateKeeper { private get; set; }

        public int TouchDamage => touchDamage;

        public Vector2 CurrentDirection => ai.velocity.normalized;

        private MonsterState CurrentState => new MonsterState(Position, DateTime.Now, investigationHistory);

        private EntityPosition Position => new EntityPosition(transform.position.x, transform.position.y);

        private bool IsMoving => ai.velocity != Vector3.zero;

        #endregion

        #region Methods

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            shadeAnimator.SetBool(MovingAnimatorKey, IsMoving);
            shadeAnimator.SetFloat(XAnimatorKey, CurrentDirection.x);
            shadeAnimator.SetFloat(YAnimatorKey, CurrentDirection.y);
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
            var investigation = StartNew(FloorStateKeeper.CurrentFloorState, CurrentState, MRng.Random());

            var currentTarget = NextTarget(investigation, Position);
            SetPathFinderTarget(currentTarget);

            while (!IsComplete(investigation))
            {
                investigation = Progress(investigation, currentTilesInVision);

                var newTarget = NextTarget(investigation, Position);
                if (!newTarget.Equals(currentTarget))
                {
                    SetPathFinderTarget(newTarget);
                    currentTarget = newTarget;
                }

                if (Mathf.Abs(Vector2.Distance(ai.destination, transform.position)) < minimumDistanceTargetLock)
                    OnMinimumDistanceEntered?.Invoke(ai.destination);
                else
                    OnMinimumDistanceLeft?.Invoke();

                yield return new WaitForSeconds(1f / investigationUpdatesPerSecond);
            }

            var completed = Complete(investigation);
            investigationHistory = investigationHistory.Add(completed);

            StartInvestigation();
        }


        private void SetPathFinderTarget(TilePosition tilePosition)
        {
            ai.destination = tilePosition.ToVector3() + new Vector3(0.5f, 0.5f, 0);
        }


        private void OnDestroy()
        {
            CancelInvestigation();
        }

        #endregion

    }

}