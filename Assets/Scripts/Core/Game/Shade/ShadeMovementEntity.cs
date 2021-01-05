using System;
using Pathfinding;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeMovementEntity : MonoBehaviour
    {

        private static readonly int movingAnimatorKey = Animator.StringToHash("IsMoving");


        [Pub] public event EventHandler<ShadeTargetReachedEventArgs> OnTargetReached;


        [FindComponent] private Animator animator;

        [FindInScene] private AIPath aiPath;

        private bool reachedTarget;


        public Vector2 CurrentDirection => aiPath.desiredVelocity.normalized;

        private bool IsMoving
        {
            set => animator.SetBool(movingAnimatorKey, value);
        }

        private bool ReachedTarget
        {
            get => reachedTarget;
            set
            {
                if (value == ReachedTarget) return;

                reachedTarget = value;
                if (ReachedTarget) OnTargetReached?.Invoke(this, new ShadeTargetReachedEventArgs());
            }
        }


        private void Update()
        {
            UpdateIsMoving();
            ReachedTarget = aiPath.reachedDestination;
        }

        private void UpdateIsMoving() =>
            IsMoving = CurrentDirection.magnitude > float.Epsilon;

        [Sub(nameof(ShadeBrainEntity.OnTargetPositionChanged))]
        private void OnTargetPositionChanged(object _, ShadeTargetPositionChangedEventArgs eventArgs) =>
            SetMovementTarget(eventArgs.NewTargetPosition);

        private void SetMovementTarget(Vector3 position)
        {
            aiPath.destination = position;
            ReachedTarget = false;
        }

    }

}