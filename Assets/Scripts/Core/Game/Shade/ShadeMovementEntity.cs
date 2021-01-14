using System;
using AChildsCourage.Game.Floors.Courage;
using Pathfinding;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeMovementEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<ShadeTargetReachedEventArgs> OnTargetReached;


        [SerializeField] private Range<float> speedRange;

        [FindInScene] private AIPath aiPath;

        private Vector2? targetPosition;
        private bool reachedTarget;


        public Vector2 CurrentDirection => aiPath.desiredVelocity.normalized;

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

        public Vector2 AiTarget
        {
            get => aiPath ? aiPath.destination : transform.position;
            private set => aiPath.destination = value;
        }

        private float Speed
        {
            set => aiPath.maxSpeed = value;
        }


        private void Update() =>
            ReachedTarget = aiPath.reachedDestination && aiPath.hasPath;

        [Sub(nameof(ShadeBrainEntity.OnCommand))]
        private void OnCommand(object _1, ShadeCommandEventArgs eventArgs)
        {
            switch (eventArgs.Command)
            {
                case MoveToCommand moveTo:
                    SetMovementTarget(moveTo.Target);
                    break;
                case StopCommand _:
                    Stop();
                    break;
            }
        }

        private void SetMovementTarget(Vector2 position)
        {
            aiPath.isStopped = false;
            if (!position.Map(IsNewTarget)) return;

            targetPosition = position;
            AiTarget = position;
            ReachedTarget = false;
        }

        private bool IsNewTarget(Vector2 position) =>
            targetPosition == null ||
            Vector2.Distance(position, targetPosition.Value) >= 0.05f;

        private void Stop()
        {
            aiPath.isStopped = true;
            targetPosition = null;
            aiPath.SetPath(null);
        }

        [Sub(nameof(CourageManagerEntity.OnCollectedCourageChanged))]
        private void OnCollectedCourageChanged(object _, CollectedCourageChangedEventArgs eventArgs) =>
            Speed = speedRange.Map(Range.Lerp, eventArgs.CompletionPercent);

    }

}