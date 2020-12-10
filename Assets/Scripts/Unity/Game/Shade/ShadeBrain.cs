using System;
using System.Collections;
using System.Collections.Generic;
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

        #region Subtypes

        private delegate IEnumerator BehaviourFunction();

        #endregion

        #region Fields

        public Events.Vector3 onTargetPositionChanged;

#pragma warning disable 649

        [SerializeField] private float behaviourUpdatesPerSecond;
        [SerializeField] private int touchDamage;
        [SerializeField] private Rigidbody2D characterRigidbody;

#pragma warning restore 649

        private readonly HashSet<TilePosition> investigatedPositions = new HashSet<TilePosition>();
        private InvestigationHistory investigationHistory = Empty;
        private Vector3 currentTargetPosition;
        private readonly InvestigationBehaviour investigationBehaviour = new InvestigationBehaviour();
        private readonly DirectHuntingBehaviour directHuntingBehaviour = new DirectHuntingBehaviour();
        private readonly IndirectHuntingBehaviour indirectHuntingBehaviour = new IndirectHuntingBehaviour();
        private Coroutine behaviourRoutine;
        private ShadeBehaviourType behaviourType;

        #endregion

        #region Properties

        [AutoInject] public FloorStateKeeper FloorStateKeeper { private get; set; }

        public int TouchDamage => touchDamage;

        public Vector3 CurrentTargetPosition
        {
            get => currentTargetPosition;
            set
            {
                currentTargetPosition = value;
                onTargetPositionChanged.Invoke(currentTargetPosition);
            }
        }


        private bool IsInvestigating => behaviourType == ShadeBehaviourType.Investigating;

        private bool IsHuntingDirectly => behaviourType == ShadeBehaviourType.DirectHunting;

        private float BehaviourUpdateWaitTime => 1f / behaviourUpdatesPerSecond;

        private TilePosition CurrentTargetTile { get => CurrentTargetPosition.FloorToTile(); set => CurrentTargetPosition = value.GetTileCenter(); }

        private ShadeState CurrentState => new ShadeState(Position, DateTime.Now, investigationHistory);

        private EntityPosition Position => new EntityPosition(transform.position.x, transform.position.y);

        #endregion

        #region Methods

        public void OnNightPrepared()
        {
            StartBehaviour(Investigate);
        }

        private void StartBehaviour(BehaviourFunction behaviourFunction)
        {
            if (behaviourRoutine != null)
                StopCoroutine(behaviourRoutine);
            behaviourRoutine = StartCoroutine(behaviourFunction());
        }


        public void OnAwarenessLevelChanged(AwarenessLevel awarenessLevel)
        {
            if (behaviourType != ShadeBehaviourType.DirectHunting && awarenessLevel == AwarenessLevel.Hunting)
                StartBehaviour(DirectHunt);
        }


        public void OnTilesInVisionChanged(TilesInView tilesInView)
        {
            investigatedPositions.UnionWith(tilesInView);
        }


        public void OnCharacterVisibilityChanged(Visibility characterVisibility)
        {
            if (characterVisibility == Visibility.NotVisible && IsHuntingDirectly)
                StartBehaviour(IndirectHunt);
        }

        private IEnumerator Investigate()
        {
            void StartInvestigation()
            {
                behaviourType = ShadeBehaviourType.Investigating;
                investigationBehaviour.StartNewInvestigation(FloorStateKeeper.CurrentFloorState, CurrentState);
                CurrentTargetTile = investigationBehaviour.CurrentTargetTile;
            }

            bool InvestigationIsInProgress() => investigationBehaviour.InvestigationIsInProgress;

            void ProgressInvestigation()
            {
                investigationBehaviour.ProgressInvestigation(CurrentState, investigatedPositions);
                investigatedPositions.Clear();

                if (!investigationBehaviour.CurrentTargetTile.Equals(CurrentTargetTile))
                    CurrentTargetTile = investigationBehaviour.CurrentTargetTile;
            }

            void CompleteInvestigation()
            {
                var completed = investigationBehaviour.CompleteInvestigation();
                investigationHistory = investigationHistory.Add(completed);

                StartBehaviour(Investigate);
            }

            StartInvestigation();

            while (InvestigationIsInProgress())
            {
                ProgressInvestigation();
                yield return new WaitForSeconds(BehaviourUpdateWaitTime);
            }

            CompleteInvestigation();
        }

        private IEnumerator DirectHunt()
        {
            void StartHunt()
            {
                behaviourType = ShadeBehaviourType.DirectHunting;
                directHuntingBehaviour.StartHunt(characterRigidbody);
            }

            bool HuntIsInProgress() => directHuntingBehaviour.HuntIsInProgress;

            void ProgressHunt()
            {
                directHuntingBehaviour.ProgressHunt();
                CurrentTargetPosition = directHuntingBehaviour.TargetPosition;
            }

            StartHunt();

            while (HuntIsInProgress())
            {
                ProgressHunt();
                yield return new WaitForSeconds(BehaviourUpdateWaitTime);
            }
        }

        private IEnumerator IndirectHunt()
        {
            void StartHunt()
            {
                behaviourType = ShadeBehaviourType.IndirectHunting;
                indirectHuntingBehaviour.StartIndirectHunt(characterRigidbody);
            }

            bool HuntIsInProgress() => indirectHuntingBehaviour.HuntIsInProgress;

            void ProgressHunt()
            {
                indirectHuntingBehaviour.ProgressHunt();
                CurrentTargetPosition = indirectHuntingBehaviour.TargetPosition;
            }

            void StopHunt()
            {
                StartBehaviour(Investigate);
            }

            StartHunt();

            while (HuntIsInProgress())
            {
                ProgressHunt();
                yield return new WaitForSeconds(BehaviourUpdateWaitTime);
            }

            StopHunt();
        }

        #endregion

    }

}