using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public readonly struct LastKnownCharInfo
    {
        
        public static Vector2 PredictPosition(float currentTime, LastKnownCharInfo info)
        {
            var elapsedTime = info.Map(CalculateElapsedTime, currentTime);
            var travelledDistance = info.Map(CalculateTravelledDistance, elapsedTime);
            return info.lastKnownPosition + travelledDistance;
        }

        public static float CalculateElapsedTime(float currentTime, LastKnownCharInfo info) =>
            currentTime - info.lastSpottedTime;

        private static Vector2 CalculateTravelledDistance(float elapsedTime, LastKnownCharInfo info) =>
            info.lastKnownVelocity * elapsedTime;


        private readonly Vector2 lastKnownPosition;
        private readonly Vector2 lastKnownVelocity;
        private readonly float lastSpottedTime;


        public LastKnownCharInfo(Vector2 lastKnownPosition, Vector2 lastKnownVelocity, float lastSpottedTime)
        {
            this.lastKnownPosition = lastKnownPosition;
            this.lastKnownVelocity = lastKnownVelocity;
            this.lastSpottedTime = lastSpottedTime;
        }

    }

}