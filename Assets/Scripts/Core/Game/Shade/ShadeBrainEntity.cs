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

namespace AChildsCourage.Game.Shade
{

    public class ShadeBrainEntity : MonoBehaviour
    {

        private static readonly int fadePropertyId = Shader.PropertyToID("_Fade");


        [Pub] public event EventHandler OnBanishingStarted;
        
        [Pub] public event EventHandler OnBanishingCompleted;

        #region Subtypes

        private delegate IEnumerator BehaviourFunction();

        #endregion

        #region Fields
        
        public Events.Vector3 onTargetPositionChanged;

#pragma warning disable 649

        [SerializeField] private float behaviourUpdatesPerSecond;
        [SerializeField] private int touchDamage;
        [SerializeField] private Rigidbody2D characterRigidbody;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material dissolveMaterial;
        [SerializeField] private new Collider2D collider;

        [FindInScene] private ShadeEyesEntity shadeEyes;
        [FindInScene] private FloorStateKeeperEntity floorStateKeeper;


#pragma warning restore 649

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
                onTargetPositionChanged.Invoke(currentTargetPosition);
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

        private void Awake() => indirectHuntingBehaviour = new IndirectHuntingBehaviour(shadeEyes);

        private void StartBehaviour(BehaviourFunction behaviourFunction)
        {
            if (behaviourRoutine != null) StopCoroutine(behaviourRoutine);
            behaviourRoutine = StartCoroutine(behaviourFunction());
        }


        public void OnAwarenessLevelChanged(AwarenessLevel awarenessLevel)
        {
            if (behaviourType != ShadeBehaviourType.DirectHunting && awarenessLevel == AwarenessLevel.Hunting) StartBehaviour(DirectHunt);
        }


        public void OnTilesInVisionChanged(TilesInView tilesInView) => investigatedPositions.UnionWith(tilesInView);


        public void OnCharacterVisibilityChanged(Visibility characterVisibility)
        {
            if (characterVisibility == Visibility.NotVisible && IsHuntingDirectly) StartBehaviour(IndirectHunt);
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
            OnBanishingStarted?.Invoke(this, EventArgs.Empty);
            StartBehaviour(None);
            CurrentTargetPosition = transform.position;
            collider.enabled = false;

            if (!isDissolving) StartCoroutine(Dissolve());
        }

        private void DeactivateShade()
        {
            transform.position = new Vector3(100, 100, 0);
            OnBanishingCompleted?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }


        public void Respawn()
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