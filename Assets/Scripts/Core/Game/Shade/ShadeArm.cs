using System;
using AChildsCourage.Game.Char;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeArm : MonoBehaviour
    {

        private static readonly int attackTriggerKey = Animator.StringToHash("Attack");


        [SerializeField] private float attackRange;
        [SerializeField] private float attackCooldown;
        [SerializeField] private float attackAttemptsPerSecond;

        [FindComponent] private Animator animator;

        [FindInScene] private CharControllerEntity @char;

        private bool charIsInRange;
        private bool attackIsOnCooldown;
        private bool isBanished;
        private bool charIsAlive = true;


        private bool CanAttack => charIsAlive && !isBanished && charIsInRange && !attackIsOnCooldown;


        private void Awake() =>
            this.DoContinually(TryAttack, 1f / attackAttemptsPerSecond);

        public void DamageChar() =>
            @char.Kill();

        [Sub(nameof(CharControllerEntity.OnPositionChanged))]
        private void OnPositionChanged(object _, CharPositionChangedEventArgs eventArgs) =>
            charIsInRange = Vector2.Distance(eventArgs.NewPosition, transform.position) <= attackRange;

        [Sub(nameof(ShadeBodyEntity.OnShadeSteppedOnRune))]
        private void OnShadeSteppedOnRune(object _1, EventArgs _2) =>
            isBanished = true;

        [Sub(nameof(ShadeSpawnerEntity.OnShadeSpawned))]
        private void OnShadeSpawned(object _1, EventArgs _2) =>
            isBanished = false;

        [Sub(nameof(CharControllerEntity.OnCharKilled))]
        private void OnCharKilled(object _1, EventArgs _2) =>
            charIsAlive = false;

        private void TryAttack()
        {
            if (CanAttack)
                Attack();
        }

        private void Attack()
        {
            animator.SetTrigger(attackTriggerKey);
            StartCooldown();
        }

        private void StartCooldown()
        {
            attackIsOnCooldown = true;
            this.DoAfter(CompleteCooldown, attackCooldown);
        }

        private void CompleteCooldown() =>
            attackIsOnCooldown = false;

    }

}