﻿using System;
using AChildsCourage.Game.Char;
using JetBrains.Annotations;
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


        private bool IsActive => !isBanished;

        private bool AttackIsReady => !attackIsOnCooldown;

        private bool CanAttack => charIsAlive && IsActive && charIsInRange && AttackIsReady;


        [Sub(nameof(ShadeBodyEntity.OnShadeActivated))] [UsedImplicitly]
        private void OnShadeActivated(object _1, EventArgs _2) =>
            this.DoContinually(TryAttack, 1f / attackAttemptsPerSecond);

        [UsedImplicitly]
        public void DamageChar() =>
            @char.Kill();

        [Sub(nameof(CharControllerEntity.OnPositionChanged))] [UsedImplicitly]
        private void OnPositionChanged(object _, CharPositionChangedEventArgs eventArgs) =>
            charIsInRange = Vector2.Distance(eventArgs.NewPosition, transform.position) <= attackRange;

        [Sub(nameof(ShadeBodyEntity.OnShadeSteppedOnRune))] [UsedImplicitly]
        private void OnShadeSteppedOnRune(object _1, EventArgs _2) =>
            isBanished = true;

        [Sub(nameof(ShadeSpawnerEntity.OnShadeSpawned))] [UsedImplicitly]
        private void OnShadeSpawned(object _1, EventArgs _2) =>
            isBanished = false;

        [Sub(nameof(CharControllerEntity.OnCharKilled))] [UsedImplicitly]
        private void OnCharKilled(object _1, EventArgs _2) =>
            charIsAlive = false;

        private void TryAttack()
        {
            if (CanAttack)
                Attack();
        }

        private void Attack()
        {
            PlayAttackAnimation();
            StartCooldown();
        }

        private void PlayAttackAnimation() =>
            animator.SetTrigger(attackTriggerKey);

        private void StartCooldown()
        {
            attackIsOnCooldown = true;
            this.DoAfter(CompleteCooldown, attackCooldown);
        }

        private void CompleteCooldown() =>
            attackIsOnCooldown = false;

    }

}