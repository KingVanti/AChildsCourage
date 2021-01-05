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

        private Vector2 Target
        {
            get => aiPath.destination;
            set => aiPath.destination = value;
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

        private void SetMovementTarget(Vector2 position)
        {
            if (!position.Map(IsNewTarget)) return;

            Target = position;
            ReachedTarget = false;
        }

        private bool IsNewTarget(Vector2 position) =>
            Vector2.Distance(position, Target) >= 0.05f;

    }

}