using System;
using UnityEngine;
using static AChildsCourage.F;

namespace AChildsCourage.Game.Shade
{

    public class ShadeHeadEntity : MonoBehaviour
    {

        private static readonly int angleAnimatorKey = Animator.StringToHash("Angle");


        [Pub] public event EventHandler<VisualContactToTargetEventArgs> OnVisualContactToTarget;


        [SerializeField] private float degreesPerSecond;

        [FindComponent(ComponentFindMode.OnParent)]
        private Animator animator;

        [FindInScene] private ShadeMovementEntity shadeMovement;
        [FindInScene] private ShadeEyesEntity shadeEyes;

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

        private bool CanSeeExplicitTarget => explicitTargetPosition.HasValue && shadeEyes.CanSee(explicitTargetPosition.Value);


        private void Update()
        {
            RotateTowardsTarget();
            UpdateVisualContact();
        }

        private void RotateTowardsTarget() =>
            CurrentDirection = Vector2.MoveTowards(CurrentDirection, TargetDirection, degreesPerSecond * Time.deltaTime);

        private void UpdateVisualContact() =>
            If(CanSeeExplicitTarget)
                .Then(() => OnVisualContactToTarget?.Invoke(this, new VisualContactToTargetEventArgs()));

    }

}