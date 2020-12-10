using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class HuntingBehaviour
    {

        private Transform target;


        public  bool HuntIsInProgress { get; private set; }
        
        public Vector3 TargetPosition => target.position;


        public void StartHunt(Transform targetTransform)
        {
            target = targetTransform;
            HuntIsInProgress = true;
        }

    }

}