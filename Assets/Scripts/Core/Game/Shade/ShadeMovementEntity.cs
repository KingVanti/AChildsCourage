using System;
using Pathfinding;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeMovementEntity : MonoBehaviour
    {

        private static readonly int movingAnimatorKey = Animator.StringToHash("IsMoving");


        [Pub] public event EventHandler<ShadeTargetReachedEventArgs> OnTargetReached;


        [SerializeField] private float pathRefreshesPerSecond;

        [FindComponent] private Animator animator;

        [FindInScene] private AIPath aiPath;

        private Vector2 targetPosition;
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
            get => aiPath.destination;
            set => aiPath.destination = value;
        }

        private void Update()
        {
            IsMoving = !aiPath.isStopped;
            ReachedTarget = aiPath.reachedDestination;
        }


        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            this.DoContinually(RecalculatePath, 1f / pathRefreshesPerSecond);

        [Sub(nameof(ShadeBrainEntity.OnTargetPositionChanged))]
        private void OnTargetPositionChanged(object _, ShadeTargetPositionChangedEventArgs eventArgs) =>
            SetMovementTarget(eventArgs.NewTargetPosition);

        private void SetMovementTarget(Vector2 position)
        {
            if (!position.Map(IsNewTarget)) return;

            targetPosition = position;
            ReachedTarget = false;
        }

        private bool IsNewTarget(Vector2 position) =>
            Vector2.Distance(position, targetPosition) >= 0.05f;

        private void RecalculatePath()
        {
            if (targetPosition != AiTarget) AiTarget = targetPosition;
        }

    }

}