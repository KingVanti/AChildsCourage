using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    [Serializable]
    public struct VisionCone
    {

#pragma warning disable 649
        
        [SerializeField] private float viewRadius;
        [SerializeField] private float viewAngle;

#pragma warning restore 649
        
        public float ViewRadius => viewRadius;

        public float ViewAngle => viewAngle;

    }

}