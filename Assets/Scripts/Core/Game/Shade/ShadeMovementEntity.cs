using System;
using Pathfinding;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeMovementEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<ShadeTargetReachedEventArgs> OnTargetReached;

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
            get => aiPath.destination;
            private set => aiPath.destination = value;
        }


        private void Update() => ReachedTarget = aiPath.reachedDestination;

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

    }

}