using System;
using System.Collections;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Shade.Navigation;
using Ninject.Extensions.Unity;
using UnityEngine;
using static AChildsCourage.Game.MEntityPosition;
using static AChildsCourage.Game.Shade.Navigation.MInvestigationHistory;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Shade
{

    [UseDi]
    public class ShadeBrain : MonoBehaviour
    {

        #region Fields

        public Events.Vector3 onTargetPositionChanged;

#pragma warning disable 649

        [SerializeField] private float behaviourUpdatesPerSecond;
        [SerializeField] private int touchDamage;
        [SerializeField] private Transform characterTransform;

#pragma warning restore 649

        private TilesInView currentTilesInVision = new TilesInView(Enumerable.Empty<TilePosition>());
        private InvestigationHistory investigationHistory = Empty;
        private Coroutine investigationCoroutine;
        private Coroutine huntingCoroutine;
        private Vector3 currentTargetPosition;
        private readonly InvestigationBehaviour investigationBehaviour = new InvestigationBehaviour();
        private readonly HuntingBehaviour huntingBehaviour = new HuntingBehaviour();

        #endregion

        #region Properties

        [AutoInject] public FloorStateKeeper FloorStateKeeper { private get; set; }

        public int TouchDamage => touchDamage;


        private bool IsCurrentlyInvestigating => investigationCoroutine != null;

        private float BehaviourUpdateWaitTime => 1f / behaviourUpdatesPerSecond;

        private TilePosition CurrentTargetTile { get => CurrentTargetPosition.FloorToTile(); set => CurrentTargetPosition = value.GetTileCenter(); }

        private Vector3 CurrentTargetPosition
        {
            get => currentTargetPosition;
            set
            {
                currentTargetPosition = value;
                onTargetPositionChanged.Invoke(currentTargetPosition);
            }
        }

        private MonsterState CurrentState => new MonsterState(Position, DateTime.Now, investigationHistory);

        private EntityPosition Position => new EntityPosition(transform.position.x, transform.position.y);

        #endregion

        #region Methods

        public void OnAwarenessLevelChanged(AwarenessLevel awarenessLevel)
        {
            if (awarenessLevel == AwarenessLevel.Hunting)
                StartHunt();
        }

        private void StartHunt()
        {
            if (IsCurrentlyInvestigating)
                CancelInvestigation();

            huntingBehaviour.StartHunt(characterTransform);
            huntingCoroutine = StartCoroutine(Hunt());
        }

        private void CancelInvestigation()
        {
            StopCoroutine(investigationCoroutine);
            investigationCoroutine = null;
        }


        public void OnTilesInVisionChanged(TilesInView tilesInView)
        {
            currentTilesInVision = tilesInView;
        }


        public void StartInvestigation()
        {
            investigationBehaviour.StartNewInvestigation(FloorStateKeeper.CurrentFloorState, CurrentState);
            CurrentTargetTile = investigationBehaviour.CurrentTargetTile;

            investigationCoroutine = StartCoroutine(Investigate());
        }

        private IEnumerator Investigate()
        {
            while (investigationBehaviour.InvestigationIsInProgress)
            {
                investigationBehaviour.ProgressInvestigation(currentTilesInVision);

                if (!investigationBehaviour.CurrentTargetTile.Equals(CurrentTargetTile))
                    CurrentTargetTile = investigationBehaviour.CurrentTargetTile;

                yield return new WaitForSeconds(BehaviourUpdateWaitTime);
            }

            CompleteInvestigation();
        }

        private void CompleteInvestigation()
        {
            var completed = investigationBehaviour.CompleteInvestigation();
            investigationHistory = investigationHistory.Add(completed);

            StartInvestigation();
        }

        private IEnumerator Hunt()
        {
            while (huntingBehaviour.HuntIsInProgress)
            {
                CurrentTargetPosition = huntingBehaviour.TargetPosition;

                yield return new WaitForSeconds(BehaviourUpdateWaitTime);
            }
        }

        #endregion

    }

}