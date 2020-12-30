using System;
using UnityEngine;
using static AChildsCourage.Game.Shade.MVisibility;

namespace AChildsCourage.Game.Shade
{

    public static class MVisionCone
    {

        public static bool Contains(VisionCone cone, ShadeHead head, Vector2 point) =>
            PointIsInRadius(point, head.Position, cone.ViewRadius) &&
            PointIsInAngle(point, head.Position, head.Direction, cone.ViewAngle) &&
            (cone.CanSeeThroughWalls ||
             HasLineOfSight(point, head.Position, head.ObstacleExistsBetween));

        private static bool PointIsInRadius(Vector2 point, Vector2 headPosition, float viewRadius) =>
            Vector2.Distance(point, headPosition) <= viewRadius;

        private static bool PointIsInAngle(Vector2 point, Vector2 headPosition, Vector2 viewDirection, float viewAngle) =>
            Vector2.Angle(viewDirection, point - headPosition) < viewAngle / 2f;

        private static bool HasLineOfSight(Vector2 point, Vector2 headPosition, Func<Vector2, Vector2, bool> obstacleExistsBetween) =>
            !obstacleExistsBetween(point, headPosition);


        [Serializable]
        public struct VisionCone
        {

            [SerializeField] private Visibility visibility;
            [SerializeField] private float viewRadius;
            [SerializeField] private float viewAngle;
            [SerializeField] private bool canSeeThroughWalls;

            
            public Visibility Visibility => visibility;

            public float ViewRadius => viewRadius;

            public float ViewAngle => viewAngle;

            public bool CanSeeThroughWalls => canSeeThroughWalls;

            
            public VisionCone(Visibility visibility, float viewRadius, float viewAngle, bool canSeeThroughWalls)
            {
                this.visibility = visibility;
                this.viewRadius = viewRadius;
                this.viewAngle = viewAngle;
                this.canSeeThroughWalls = canSeeThroughWalls;
            }

        }

    }

}