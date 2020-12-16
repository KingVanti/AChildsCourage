using Ninject.Extensions.Unity;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    [UseDi]
    public class ShadeHeadEntity : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private float rotationDegreesPerSecond;

#pragma warning restore 649

        #endregion

        #region Properties

        [AutoInject] public ShadeMovementEntity Movement { private get; set; }

        #endregion

        #region Methods

        private void Update() => FaceMovementDirection();

        private void FaceMovementDirection() => transform.right = Movement.CurrentDirection;

        #endregion

    }

}