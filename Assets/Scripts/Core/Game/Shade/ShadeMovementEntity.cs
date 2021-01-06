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

        private Vector2? targetPosition;
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

        private Vector2 AiTarget
        {
            set => aiPath.destination = value;
        }
        

        private void Update()
        {
            IsMoving = !aiPath.isStopped;
            ReachedTarget = aiPath.reachedDestination;
        }

        [Sub(nameof(ShadeBrainEntity.OnMoveTargetChanged))]
        private void OnMoveTargetChanged(object _, ShadeMoveTargetChangedEventArgs eventArgs)
        {
            aiPath.isStopped = !eventArgs.NewTargetPosition.HasValue;

            if (eventArgs.NewTargetPosition.HasValue)
                SetMovementTarget(eventArgs.NewTargetPosition.Value);
            else
            {
                targetPosition = null;
                aiPath.SetPath(null);
            }
        }

        private void SetMovementTarget(Vector2 position)
        {
            if (!position.Map(IsNewTarget)) return;

            targetPosition = position;
            AiTarget = position;
            ReachedTarget = false;
        }

        private bool IsNewTarget(Vector2 position) =>
            targetPosition == null ||
            Vector2.Distance(position, targetPosition.Value) >= 0.05f;
        

    }

}