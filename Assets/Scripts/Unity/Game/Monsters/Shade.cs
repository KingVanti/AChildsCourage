using System;
using System.Collections;
using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Monsters.Navigation;
using UnityEngine;
using Pathfinding;
using static AChildsCourage.Game.Monsters.Navigation.MInvestigation;
using static AChildsCourage.Game.Monsters.Navigation.MInvestigationHistory;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Monsters
{

    public class Shade : MonoBehaviour
    {
        #region Fields

        public Path path;
        public AIBase aiBase;
        public AIPath aiPath;

#pragma warning disable 649
        [SerializeField] private FloorStateKeeper floorStateKeeper;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Seeker seeker;
        [Header("Stats")][SerializeField] private int touchDamage;
        [SerializeField] private int attackDamage;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float investigationUpdatesPerSecond;
        [SerializeField] private Transform targetTransform;
#pragma warning restore 649

        private IEnumerable<TilePosition> currentTilesInVision;
        private InvestigationHistory investigationHistory = Empty;

        private Coroutine investigationCoroutine;
        private int currentWaypoint = 0;
        private float nextWaypointDistance = 1f;
        private bool reachedEndOfPath = false;

        #endregion

        #region Properties

        private MonsterState CurrentState => new MonsterState(Position, DateTime.Now, investigationHistory);

        private EntityPosition Position => new EntityPosition(transform.position.x, transform.position.y);

        private FloorState FloorState => floorStateKeeper.CurrentFloorState;

        private Vector2 MoveVector {
            get {
                return rb.velocity;
            }
        }
        
        private Vector2 NextTargetDestination {
            get; set;
        }

        #endregion

        #region Methods

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
                    aiPath.SearchPath();
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
            //targetTransform.position = tilePosition.ToVector3() + new Vector3(0.5f, 0.5f, 0);
            aiPath.destination = tilePosition.ToVector3() + new Vector3(0.5f, 0.5f, 0);
            //Vector3 deltaPos = aiPath.desiredVelocity - transform.position;
            //transform.GetChild(0).right = 
        }


        private void OnDestroy() {
            CancelInvestigation();
        }

        #endregion

    }

}