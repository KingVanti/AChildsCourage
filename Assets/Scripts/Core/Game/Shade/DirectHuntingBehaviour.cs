using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class DirectHuntingBehaviour
    {

        private Rigidbody2D target;


        public bool HuntIsInProgress { get; private set; }

        public Vector3 TargetPosition { get; private set; }


        public void StartHunt(Rigidbody2D targetRigidbody)
        {
            target = targetRigidbody;
            HuntIsInProgress = true;
        }

        public void ProgressHunt() =>
            TargetPosition = target.position;

    }

}