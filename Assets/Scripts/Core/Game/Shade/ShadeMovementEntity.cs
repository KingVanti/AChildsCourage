using System;
using System.Collections;
using AChildsCourage.Infrastructure;
using Pathfinding;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeMovementEntity : MonoBehaviour
    {

        #region Static Fields

        private static readonly int movingAnimatorKey = Animator.StringToHash("IsMoving");
        private static readonly int xAnimatorKey = Animator.StringToHash("X");
        private static readonly int yAnimatorKey = Animator.StringToHash("Y");

        #endregion

        #region Fields

#pragma warning disable 649

        [SerializeField] private float movementSpeed;
        [SerializeField] private float waitTimeAfterDealingDamage;
        [SerializeField] private AIPath aiPath;
        [SerializeField] private Animator shadeAnimator;

#pragma warning restore 649

        private float standardSpeed;

        #endregion

        #region Properties

        public Vector2 CurrentDirection => aiPath.desiredVelocity.normalized;


        private bool IsMoving => aiPath.velocity != Vector3.zero;

        #endregion

        #region Methods

        [Sub(nameof(ShadeBrainEntity.OnTargetPositionChanged))]
        private void OnTargetPositionChanged(object _, ShadeTargetPositionChangedEventArgs eventArgs) => SetMovementTarget(eventArgs.NewTargetPosition);

        private void SetMovementTarget(Vector3 position) => aiPath.destination = position;


        [Sub(nameof(ShadeSpawnerEntity.OnShadeSpawned))]
        private void OnShadeSpawned(object _1, EventArgs _2) => ResetSpeed();

        private void ResetSpeed() => aiPath.maxSpeed = movementSpeed;


        private void Update() => UpdateAnimator();

        private void UpdateAnimator()
        {
            shadeAnimator.SetBool(movingAnimatorKey, IsMoving);
            shadeAnimator.SetFloat(xAnimatorKey, CurrentDirection.x);
            shadeAnimator.SetFloat(yAnimatorKey, CurrentDirection.y);
        }

        public void WaitAfterDealingDamage() => StartCoroutine(WaitAndContinue());

        private IEnumerator WaitAndContinue()
        {
            aiPath.maxSpeed = 0.0001f;

            yield return new WaitForSeconds(waitTimeAfterDealingDamage);

            ResetSpeed();
        }

        #endregion

    }

}