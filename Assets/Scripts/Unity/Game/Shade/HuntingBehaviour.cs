using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class HuntingBehaviour
    {

        private bool hasVisualContact;
        private Vector3 lastSeenPosition;
        private float lastSeenTime;
        private Vector2 lastSeenVelocity;
        private Rigidbody2D target;


        public bool HuntIsInProgress { get; private set; }

        public Vector3 TargetPosition { get; private set; }


        private Vector3 PredictedPosition => lastSeenPosition + (Vector3) (lastSeenVelocity * (Time.deltaTime * TimeSinceLastSeen));

        private float TimeSinceLastSeen => Time.time - lastSeenTime;


        public void StartHunt(Rigidbody2D targetRigidbody)
        {
            target = targetRigidbody;
            HuntIsInProgress = true;
            hasVisualContact = true;
        }


        public void ProgressHunt()
        {
            if (hasVisualContact)
                TargetPosition = target.position;
            else
                TargetPosition = PredictedPosition;
        }


        public void OnLostPlayer()
        {
            hasVisualContact = false;

            lastSeenPosition = target.position;
            lastSeenVelocity = target.velocity;
            lastSeenTime = Time.time;
        }

    }

}