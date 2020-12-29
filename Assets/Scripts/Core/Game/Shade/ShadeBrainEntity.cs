using System;
using System.Collections;
using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Shade.Navigation;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.Game.MEntityPosition;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.Game.Shade.MVisibility;
using static AChildsCourage.Game.Shade.Navigation.MInvestigationHistory;
using static AChildsCourage.F;

namespace AChildsCourage.Game.Shade
{

    public class ShadeBrainEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<ShadeTargetPositionChangedEventArgs> OnTargetPositionChanged;

        #region Subtypes

        private delegate IEnumerator BehaviourFunction();

        #endregion

        #region Fields

        [SerializeField] private float behaviourUpdatesPerSecond;
        [SerializeField] private int touchDamage;
        [SerializeField] private Rigidbody2D characterRigidbody;

        [FindInScene] private ShadeEyesEntity shadeEyes;
        [FindInScene] private FloorStateKeeperEntity floorStateKeeper;

        private readonly HashSet<TilePosition> investigatedPositions = new HashSet<TilePosition>();
        private InvestigationHistory investigationHistory = Empty;
        private Vector3 currentTargetPosition;
        private readonly InvestigationBehaviour investigationBehaviour = new InvestigationBehaviour();
        private readonly DirectHuntingBehaviour directHuntingBehaviour = new DirectHuntingBehaviour();
        private IndirectHuntingBehaviour indirectHuntingBehaviour;
        private Coroutine behaviourRoutine;
        private ShadeBehaviourType behaviourType;

        #endregion

        #region Properties

        public int TouchDamage => touchDamage;

        public Vector2 CurrentTargetPosition
        {
            get => currentTargetPosition;
            private set
            {
                currentTargetPosition = value;
                OnTargetPositionChanged?.Invoke(this, new ShadeTargetPositionChangedEventArgs(currentTargetPosition));
            }
        }


        private bool IsHuntingDirectly => behaviourType == ShadeBehaviourType.DirectHunting;

        private float BehaviourUpdateWaitTime => 1f / behaviourUpdatesPerSecond;

        private TilePosition CurrentTargetTile
        {
            get => CurrentTargetPosition.Map(ToTile);
            set => CurrentTargetPosition = value.Map(GetTileCenter);
        }

        private ShadeState CurrentState => new ShadeState(Position, DateTime.Now, investigationHistory);

        private EntityPosition Position => new EntityPosition(transform.position.x, transform.position.y);

        #endregion

        #region Methods

        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            indirectHuntingBehaviour = new IndirectHuntingBehaviour(shadeEyes);

        private void StartBehaviour(BehaviourFunction behaviourFunction) =>
            behaviourRoutine = this.RestartCoroutine(behaviourRoutine, behaviourFunction.Invoke);


        [Sub(nameof(ShadeAwarenessEntity.OnShadeAwarenessChanged))]
        private void OnAwarenessLevelChanged(object _, AwarenessChangedEventArgs eventArgs) =>
            If(ShouldStartDirectHunt(eventArgs.Level))
                .Then(() => StartBehaviour(DirectHunt));

        private bool ShouldStartDirectHunt(AwarenessLevel level) =>
            behaviourType != ShadeBehaviourType.DirectHunting && level == AwarenessLevel.Hunting;


        [Sub(nameof(ShadeEyesEntity.OnTilesInViewChanged))]
        private void OnTilesInVisionChanged(object _, TilesInViewChangedEventArgs eventArgs) =>
            investigatedPositions.UnionWith(eventArgs.TilesInView);


        [Sub(nameof(ShadeEyesEntity.OnCharVisibilityChanged))]
        private void OnCharVisibilityChanged(object _, CharVisibilityChangedEventArgs eventArgs) =>
            If(ShouldStartIndirectHunt(eventArgs.CharVisibility))
                .Then(() => StartBehaviour(IndirectHunt));

        private bool ShouldStartIndirectHunt(Visibility charVisibility) =>
            charVisibility == Visibility.NotVisible && IsHuntingDirectly;

        [Sub(nameof(ShadeSpawnerEntity.OnShadeSpawned))]
        private void OnShadeSpawned(object _1, EventArgs _2) =>
            StartBehaviour(Investigate);


        private IEnumerator Investigate()
        {
            void StartInvestigation()
            {
                behaviourType = ShadeBehaviourType.Investigating;
                investigationBehaviour.StartNewInvestigation(floorStateKeeper.CurrentFloorState, CurrentState);
                CurrentTargetTile = investigationBehaviour.CurrentTargetTile;
            }

            bool InvestigationIsInProgress() => investigationBehaviour.InvestigationIsInProgress;

            void ProgressInvestigation()
            {
                investigationBehaviour.ProgressInvestigation(CurrentState, investigatedPositions);
                investigatedPositions.Clear();

                if (!investigationBehaviour.CurrentTargetTile.Equals(CurrentTargetTile)) CurrentTargetTile = investigationBehaviour.CurrentTargetTile;
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

            void StopHunt() => StartBehaviour(Investigate);

            StartHunt();

            while (HuntIsInProgress())
            {
                ProgressHunt();
                yield return new WaitForSeconds(BehaviourUpdateWaitTime);
            }

            StopHunt();
        }

        private static IEnumerator None() { yield return null; }

        #endregion

    }

}