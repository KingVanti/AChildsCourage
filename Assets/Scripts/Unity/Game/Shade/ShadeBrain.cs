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

        [SerializeField] private float investigationUpdatesPerSecond;
        [SerializeField] private int touchDamage;

#pragma warning restore 649

        private TilesInView currentTilesInVision = new TilesInView(Enumerable.Empty<TilePosition>());
        private InvestigationHistory investigationHistory = Empty;
        private Coroutine investigationCoroutine;
        private TilePosition currentTargetTile;
        private readonly InvestigationBehaviour investigationBehaviour = new InvestigationBehaviour();

        #endregion

        #region Properties

        [AutoInject] public FloorStateKeeper FloorStateKeeper { private get; set; }

        public int TouchDamage => touchDamage;


        private bool IsCurrentlyInvestigating => investigationCoroutine != null;

        private float InvestigationUpdateWaitTime => 1f / investigationUpdatesPerSecond;

        private TilePosition CurrentTargetTile
        {
            get => currentTargetTile;
            set
            {
                currentTargetTile = value;
                onTargetPositionChanged.Invoke(CurrentTargetPosition);
            }
        }

        private Vector3 CurrentTargetPosition => currentTargetTile.GetTileCenter();

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


        private void CancelInvestigation()
        {
            StopCoroutine(investigationCoroutine);
            investigationCoroutine = null;
        }
        
        
        private IEnumerator Investigate()
        {
            while (investigationBehaviour.InvestigationIsInProgress)
            {
                investigationBehaviour.ProgressInvestigation(currentTilesInVision);

                if (!investigationBehaviour.CurrentTargetTile.Equals(CurrentTargetTile))
                    CurrentTargetTile = investigationBehaviour.CurrentTargetTile;

                yield return new WaitForSeconds(InvestigationUpdateWaitTime);
            }

            CompleteInvestigation();
        }

        private void CompleteInvestigation()
        {
            var completed = investigationBehaviour.CompleteInvestigation();
            investigationHistory = investigationHistory.Add(completed);

            StartInvestigation();
        }

        private void OnDestroy() => CancelInvestigation();

        #endregion

    }

}