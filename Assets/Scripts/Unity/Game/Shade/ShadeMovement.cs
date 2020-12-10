using System.Linq;
using AChildsCourage.Game.Shade.Navigation;
using Ninject.Extensions.Unity;
using Pathfinding;
using UnityEngine;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Shade
{

    [UseDi]
    public class ShadeMovement : MonoBehaviour
    {
        
        #region Static Fields

        private static readonly int MovingAnimatorKey = Animator.StringToHash("IsMoving");
        private static readonly int XAnimatorKey = Animator.StringToHash("X");
        private static readonly int YAnimatorKey = Animator.StringToHash("Y");

        #endregion
        
        #region Fields

#pragma warning disable 649
        
        [SerializeField] private AIPath aiPath;
        [SerializeField] private Animator shadeAnimator;
        
#pragma warning restore 649
        
        #endregion

        #region Properties

        public Vector2 CurrentDirection => aiPath.desiredVelocity.normalized;
        
        
        private bool IsMoving => aiPath.velocity != Vector3.zero;

        #endregion

        #region Methods

        public void SetMovementTarget(Vector3 position)
        {
            aiPath.destination = position;
        }


        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            shadeAnimator.SetBool(MovingAnimatorKey, IsMoving);
            shadeAnimator.SetFloat(XAnimatorKey, CurrentDirection.x);
            shadeAnimator.SetFloat(YAnimatorKey, CurrentDirection.y);
        }

        #endregion

    }

}