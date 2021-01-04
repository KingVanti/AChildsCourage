using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeMovementEntity : MonoBehaviour
    {

        private static readonly int movingAnimatorKey = Animator.StringToHash("IsMoving");
        private static readonly int xAnimatorKey = Animator.StringToHash("X");
        private static readonly int yAnimatorKey = Animator.StringToHash("Y");
        
        [FindComponent] private Animator animator;

        [FindInScene] private AIPath aiPath;


        public Vector2 CurrentDirection => aiPath.desiredVelocity.normalized;

        private bool IsMoving => CurrentDirection.magnitude > float.Epsilon;


        private void Update() =>
            UpdateAnimator();

        private void UpdateAnimator()
        {
            animator.SetBool(movingAnimatorKey, IsMoving);
            animator.SetFloat(xAnimatorKey, CurrentDirection.x);
            animator.SetFloat(yAnimatorKey, CurrentDirection.y);
        }

        [Sub(nameof(ShadeBrainEntity.OnTargetPositionChanged))]
        private void OnTargetPositionChanged(object _, ShadeTargetPositionChangedEventArgs eventArgs) =>
            SetMovementTarget(eventArgs.NewTargetPosition);

        private void SetMovementTarget(Vector3 position) =>
            aiPath.destination = position;

    }

}