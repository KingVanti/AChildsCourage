using AChildsCourage.Infrastructure;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{
    
    public class ShadeHeadEntity : MonoBehaviour
    {

        #region Fields



        [SerializeField] private float rotationDegreesPerSecond;
        
        [FindInScene] private ShadeMovementEntity shadeMovement;



        #endregion

        #region Methods

        private void Update() => FaceMovementDirection();

        private void FaceMovementDirection() => transform.right = shadeMovement.CurrentDirection;

        #endregion

    }

}