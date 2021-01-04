using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeHeadEntity : MonoBehaviour
    {

        private const float DownAngle = 270;


        private static readonly int angleAnimatorKey = Animator.StringToHash("Angle");

        [FindComponent(ComponentFindMode.OnParent)]
        private Animator animator;

        [FindInScene] private ShadeMovementEntity shadeMovement;


        private float Angle
        {
            set => animator.SetFloat(angleAnimatorKey, value);
        }

        private Vector2 CurrentDirection => shadeMovement.CurrentDirection;

        private bool IsMoving => CurrentDirection.magnitude > float.Epsilon;

        private float CurrentMovementAngle => Vector2.SignedAngle(Vector2.right, CurrentDirection);


        private void Update() =>
            FaceMovementDirection();

        private void FaceMovementDirection()
        {
            transform.right = CurrentDirection;
            Angle = ChooseCurrentAngle();
        }

        private float ChooseCurrentAngle() =>
            IsMoving
                ? CurrentMovementAngle
                : DownAngle;

    }

}