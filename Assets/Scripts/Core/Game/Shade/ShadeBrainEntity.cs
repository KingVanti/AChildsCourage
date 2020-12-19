using System;
using System.Collections;
using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Shade.Navigation;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.Game.MEntityPosition;
using static AChildsCourage.Game.Shade.Navigation.MInvestigationHistory;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.Game.Shade.MVisibility;

namespace AChildsCourage.Game.Shade
{

    public class ShadeBrainEntity : MonoBehaviour
    {

        private static readonly int fadePropertyId = Shader.PropertyToID("_Fade");


        [Pub] public event EventHandler OnShadeSteppedOnRune;

        [Pub] public event EventHandler OnShadeBanished;

        [Pub] public event EventHandler<ShadeTargetPositionChangedEventArgs> OnTargetPositionChanged;

        #region Subtypes

        private delegate IEnumerator BehaviourFunction();

        #endregion

        #region Fields



        [SerializeField] private float behaviourUpdatesPerSecond;
        [SerializeField] private int touchDamage;
        [SerializeField] private Rigidbody2D characterRigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material dissolveMaterial;
        [SerializeField] private new Collider2D collider;

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
        private bool isDissolving;

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
        private void OnSceneLoaded(object _1, EventArgs _2) => indirectHuntingBehaviour = new IndirectHuntingBehaviour(shadeEyes);

        private void StartBehaviour(BehaviourFunction behaviourFunction)
        {
            if (behaviourRoutine != null) StopCoroutine(behaviourRoutine);
            behaviourRoutine = StartCoroutine(behaviourFunction());
        }


        [Sub(nameof(ShadeAwarenessEntity.OnShadeAwarenessChanged))]
        private void OnAwarenessLevelChanged(object _, AwarenessChangedEventArgs eventArgs)
        {
            if (behaviourType != ShadeBehaviourType.DirectHunting && eventArgs.Level == AwarenessLevel.Hunting) StartBehaviour(DirectHunt);
        }


        [Sub(nameof(ShadeEyesEntity.OnTilesInViewChanged))]
        private void OnTilesInVisionChanged(object _, TilesInViewChangedEventArgs eventArgs) => investigatedPositions.UnionWith(eventArgs.TilesInView);


        [Sub(nameof(ShadeEyesEntity.OnCharVisibilityChanged))]
        private void OnCharVisibilityChanged(object _, CharVisibilityChangedEventArgs eventArgs)
        {
            if (eventArgs.CharVisibility == Visibility.NotVisible && IsHuntingDirectly) StartBehaviour(IndirectHunt);
        }


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


        public void Banish()
        {
            OnShadeSteppedOnRune?.Invoke(this, EventArgs.Empty);
            StartBehaviour(None);
            CurrentTargetPosition = transform.position;
            collider.enabled = false;

            if (!isDissolving) StartCoroutine(Dissolve());
        }

        private void DeactivateShade()
        {
            transform.position = new Vector3(100, 100, 0);
            OnShadeBanished?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }


        [Sub(nameof(ShadeSpawnerEntity.OnShadeSpawned))]
        private void OnShadeSpawned(object _1, EventArgs _2) => Activate();

        private void Activate()
        {
            gameObject.SetActive(true);
            collider.enabled = true;
            StartBehaviour(Investigate);
        }

        private IEnumerator Dissolve()
        {
            isDissolving = true;

            spriteRenderer.material = dissolveMaterial;
            spriteRenderer.material.SetFloat(fadePropertyId, 1);

            while (spriteRenderer.material.GetFloat(fadePropertyId) > 0)
            {
                spriteRenderer.material.SetFloat(fadePropertyId,
                                                 Mathf.MoveTowards(spriteRenderer.material.GetFloat(fadePropertyId), 0, Time.deltaTime));
                yield return null;
            }

            DeactivateShade();

            spriteRenderer.material.SetFloat(fadePropertyId, 1);
            spriteRenderer.material = defaultMaterial;

            isDissolving = false;
        }

        #endregion

    }

}