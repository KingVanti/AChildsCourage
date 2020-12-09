using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Monsters
{

    public class ShadeEyes : MonoBehaviour
    {

        #region Subtypes

        [Serializable]
        public class VisibilityEvent : UnityEvent<Visibility> { }

        #endregion

        #region Fields

        public VisibilityEvent onCharacterVisibilityChanged;

#pragma warning disable 649

        [SerializeField] private float updatesPerSecond;
        [SerializeField] private float viewRadius;
        [SerializeField] private float viewAngle;
        [SerializeField] private LayerMask obstructionLayers;
        [SerializeField] private Transform[] characterVisionPoints;

#pragma warning restore 649

        private Visibility characterVisibility;

        #endregion

        #region Properties

        public Visibility CharacterVisibility
        {
            get => characterVisibility;
            private set
            {
                if (characterVisibility == value)
                    return;

                characterVisibility = value;
                onCharacterVisibilityChanged.Invoke(characterVisibility);
            }
        }

        public float ViewRadius => viewRadius;

        public float ViewAngle => viewAngle;

        private float WaitTime => 1f / updatesPerSecond;

        private Vector3 CurrentPosition => transform.position;

        private IEnumerable<Vector3> CurrentCharacterVisionPoints => characterVisionPoints.Select(p => p.position);

        #endregion

        #region Methods

        private void Awake()
        {
            StartCoroutine(ContinuallyUpdateVision());
        }

        private IEnumerator ContinuallyUpdateVision()
        {
            while (true)
            {
                yield return new WaitForSeconds(WaitTime);
                UpdateVision();
            }
        }

        private void UpdateVision() => CharacterVisibility = CurrentCharacterVisionPoints.Any(IsInView) ? Visibility.Primary : Visibility.NotVisible;

        private bool IsInView(Vector3 visionPoint) =>
            IsInViewRadius(visionPoint) &&
            IsInViewAngle(visionPoint) &&
            IsUnobstructed(visionPoint);

        private bool IsInViewRadius(Vector3 visionPoint) => Vector3.Distance(CurrentPosition, visionPoint) <= viewRadius;

        private bool IsInViewAngle(Vector3 visionPoint)
        {
            var dirToPoint = visionPoint - CurrentPosition;
            return Vector3.Angle(transform.right, dirToPoint) < viewAngle / 2f;
        }

        private bool IsUnobstructed(Vector3 visionPoint)
        {
            var dirToPoint = visionPoint - CurrentPosition;
            return !Physics2D.Raycast(CurrentPosition, dirToPoint, dirToPoint.magnitude, obstructionLayers);
        }

        #endregion

    }

}