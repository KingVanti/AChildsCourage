using System;
using System.Collections;
using UnityEngine;
using static AChildsCourage.Game.TilePosition;
using static AChildsCourage.Game.Shade.Visibility;
using static AChildsCourage.F;
using static AChildsCourage.Game.Shade.Investigation;

namespace AChildsCourage.Game.Shade
{

    public class ShadeBrainEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<ShadeTargetPositionChangedEventArgs> OnTargetPositionChanged;


        [SerializeField] private float behaviourUpdatesPerSecond;
        [SerializeField] private Rigidbody2D characterRigidbody;

        [FindInScene] private ShadeEyesEntity shadeEyes;

        private Vector3 currentTargetPosition;
        private readonly DirectHuntingBehaviour directHuntingBehaviour = new DirectHuntingBehaviour();
        private IndirectHuntingBehaviour indirectHuntingBehaviour;
        private Coroutine behaviourRoutine;
        private ShadeBehaviourType behaviourType;

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


        public void StartInvestigation(Aoi aoi) =>
            StartCoroutine(Investigate(aoi));

        private IEnumerator Investigate(Aoi aoi)
        {
            var investigation = Start(aoi);

            while (!investigation.Map(IsComplete))
            {
                CurrentTargetPosition = investigation
                                        .Map(GetCurrentTarget)
                                        .Position;

                yield return new WaitUntil(() => Vector2.Distance(transform.position, CurrentTargetPosition) < 1);

                investigation = investigation.Map(Progress);
            }
        }


        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            indirectHuntingBehaviour = new IndirectHuntingBehaviour(shadeEyes);

        private void StartBehaviour(Func<IEnumerator> behaviourFunction) =>
            behaviourRoutine = this.RestartCoroutine(behaviourRoutine, behaviourFunction);


        [Sub(nameof(ShadeAwarenessEntity.OnShadeAwarenessChanged))]
        private void OnAwarenessLevelChanged(object _, AwarenessChangedEventArgs eventArgs) =>
            If(ShouldStartDirectHunt(eventArgs.Level))
                .Then(() => StartBehaviour(DirectHunt));

        private bool ShouldStartDirectHunt(AwarenessLevel level) =>
            behaviourType != ShadeBehaviourType.DirectHunting && level == AwarenessLevel.Hunting;

        [Sub(nameof(ShadeEyesEntity.OnCharVisibilityChanged))]
        private void OnCharVisibilityChanged(object _, CharVisibilityChangedEventArgs eventArgs) =>
            If(ShouldStartIndirectHunt(eventArgs.CharVisibility))
                .Then(() => StartBehaviour(IndirectHunt));

        private bool ShouldStartIndirectHunt(Visibility charVisibility) =>
            charVisibility.Equals(NotVisible) && IsHuntingDirectly;

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

            void StopHunt() { }

            StartHunt();

            while (HuntIsInProgress())
            {
                ProgressHunt();
                yield return new WaitForSeconds(BehaviourUpdateWaitTime);
            }

            StopHunt();
        }

        private static IEnumerator None() { yield return null; }

    }

}