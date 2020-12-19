using AChildsCourage.Infrastructure;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeHeadEntity : MonoBehaviour
    {
        
        [FindInScene] private ShadeMovementEntity shadeMovement;


        private void Update() => FaceMovementDirection();

        private void FaceMovementDirection() => transform.right = shadeMovement.CurrentDirection;

    }

}