using AChildsCourage.Infrastructure;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{
    
    public class ShadeHeadEntity : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private float rotationDegreesPerSecond;
        
        [FindInScene] private ShadeMovementEntity shadeMovement;

#pragma warning restore 649

        #endregion

        #region Methods

        private void Update() => FaceMovementDirection();

        private void FaceMovementDirection() => transform.right = shadeMovement.CurrentDirection;

        #endregion

    }

}