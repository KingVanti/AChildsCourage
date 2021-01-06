using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeHeadEntity : MonoBehaviour
    {

        private static readonly int angleAnimatorKey = Animator.StringToHash("Angle");


        [SerializeField] private float degreesPerSecond;

        [FindComponent(ComponentFindMode.OnParent)]
        private Animator animator;

        [FindInScene] private ShadeMovementEntity shadeMovement;

        private readonly Vector2? explicitTargetPosition;


        private float Angle
        {
            set => animator.SetFloat(angleAnimatorKey, value);
        }

        private Vector2 CurrentMovementDirection => shadeMovement.CurrentDirection;

        private bool IsMoving => CurrentMovementDirection.magnitude > float.Epsilon;

        private Vector2? ExplicitFaceDirection => explicitTargetPosition.HasValue
            ? explicitTargetPosition.Value - (Vector2) transform.position
            : (Vector2?) null;

        private Vector2 MovementFaceDirection => IsMoving
            ? CurrentMovementDirection
            : Vector2.down;

        private Vector2 TargetDirection => ExplicitFaceDirection ?? MovementFaceDirection;

        private Vector2 CurrentDirection
        {
            get => transform.right;
            set
            {
                var transformAngle = Mathf.Atan2(value.y, value.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(transformAngle, Vector3.forward);
                Angle = Vector2.SignedAngle(Vector2.right, CurrentDirection);
            }
        }


        private void Update() => RotateTowardsTarget();

        private void RotateTowardsTarget() =>
            CurrentDirection = Vector2.MoveTowards(CurrentDirection, TargetDirection, degreesPerSecond * Time.deltaTime);

    }

}