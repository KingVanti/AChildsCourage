using System;
using UnityEngine;
using static AChildsCourage.CustomMath;
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

        private Vector2? explicitTargetPosition;


        private float Angle
        {
            get => transform.eulerAngles.z;
            set
            {
                transform.eulerAngles = new Vector3(0, 0, value);
                animator.SetFloat(angleAnimatorKey, value.Map(AsSignedAngle));
            }
        }

        private Vector2 CurrentMovementDirection => shadeMovement.CurrentDirection;

        private bool IsMoving => CurrentMovementDirection.magnitude > float.Epsilon;

        private Vector2? ExplicitFaceDirection => explicitTargetPosition.HasValue
            ? explicitTargetPosition.Value - (Vector2) transform.position
            : (Vector2?) null;

        private Vector2 MovementFaceDirection => IsMoving
            ? CurrentMovementDirection
            : Vector2.down;

        private Vector2 TargetDirection => (ExplicitFaceDirection ?? MovementFaceDirection).normalized;

        private float TargetAngle => CalculateAngle(TargetDirection);

        private bool CanSeeExplicitTarget => explicitTargetPosition.HasValue && shadeEyes.CanSee(explicitTargetPosition.Value);


        private void Update()
        {
            RotateTowardsTarget();
            UpdateVisualContact();
        }

        private void RotateTowardsTarget() =>
            Angle = Mathf.MoveTowardsAngle(Angle, TargetAngle, degreesPerSecond * Time.deltaTime);

        private void UpdateVisualContact() =>
            If(CanSeeExplicitTarget)
                .Then(() => OnVisualContactToTarget?.Invoke(this, new VisualContactToTargetEventArgs()));

        [Sub(nameof(ShadeBrainEntity.OnLookTargetChanged))]
        private void OnLookTargetChanged(object _, ShadeLookTargetChangedEventArgs eventArgs) =>
            explicitTargetPosition = eventArgs.NewTargetPosition;

    }

}