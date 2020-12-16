﻿using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class IndirectHuntingBehaviour
    {
        
        private const int HuntCancelTime = 5;
        
        private ShadeEyes shadeEyes;
        private Vector3 lastSeenPosition;
        private float lastSeenTime;
        private Vector2 lastSeenVelocity;

        
        public IndirectHuntingBehaviour(ShadeEyes shadeEyes)
        {
            this.shadeEyes = shadeEyes;
        }
        
        
        public Vector3 TargetPosition { get; private set; }
        
        public  bool HuntIsInProgress { get; private set; }
        
        
        private Vector3 PredictedPosition => lastSeenPosition + (Vector3) (lastSeenVelocity * TimeSinceLastSeen);

        private float TimeSinceLastSeen => Time.time - lastSeenTime;


        private bool SearchedEnoughTime => TimeSinceLastSeen >= HuntCancelTime;

        private bool CanSeeTarget => shadeEyes.PositionIsVisible(PredictedPosition);

        
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

            HuntIsInProgress = !SearchedEnoughTime && !CanSeeTarget;
        }

    }

}