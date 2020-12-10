using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class IndirectHuntingBehaviour
    {
        
        private Vector3 lastSeenPosition;
        private float lastSeenTime;
        private Vector2 lastSeenVelocity;
        
        
        public Vector3 TargetPosition { get; private set; }
        
        public  bool HuntIsInProgress { get; private set; }
        
        
        private Vector3 PredictedPosition => lastSeenPosition + (Vector3) (lastSeenVelocity * (Time.deltaTime * TimeSinceLastSeen));

        private float TimeSinceLastSeen => Time.time - lastSeenTime;

        
        public void StartIndirectHunt(Rigidbody2D target)
        {
            lastSeenPosition = target.position;
            lastSeenVelocity = target.velocity;
            lastSeenTime = Time.time;

            HuntIsInProgress = true;
        }


        public void ProgressHunt()
        {
            TargetPosition = PredictedPosition;
        }
    }

}