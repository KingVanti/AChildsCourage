using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Monsters
{

    public class ShadeEyes : MonoBehaviour
    {

        #region Fields

        public Events.Bool onCharacterInVisionChanged;

#pragma warning  disable 649

        [SerializeField] private float updatesPerSecond;
        [SerializeField] private float viewRadius;
        [SerializeField] private float viewAngle;
        [SerializeField] private LayerMask obstructionLayers;
        [SerializeField] private Transform[] characterVisionPoints;

#pragma warning  restore 649

        private bool characterIsInVision;

        #endregion

        #region Properties

        public bool CharacterIsInVision
        {
            get => characterIsInVision;
            private set
            {
                if (characterIsInVision == value)
                    return;
                
                characterIsInVision = value;
                onCharacterInVisionChanged.Invoke(characterIsInVision);
            }
        }

        public float ViewRadius => viewRadius;

        public float ViewAngle => viewAngle;

        private int WaitTime => (int) (1f / updatesPerSecond * 1000);

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

        private void UpdateVision() => CharacterIsInVision = CurrentCharacterVisionPoints.Any(IsInView);

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