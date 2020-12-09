using Ninject.Extensions.Unity;
using UnityEngine;

namespace AChildsCourage.Game.Monsters
{

    [UseDi]
    public class ShadeHead : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private float rotationDegreesPerSecond;

#pragma warning restore 649

        #endregion

        #region Properties

        [AutoInject] public Shade Shade { private get; set; }

        #endregion

        #region Methods

        private void Update()
        {
            FaceMovementDirection();
        }

        private void FaceMovementDirection()
        {
            transform.right = Shade.CurrentDirection;
        }

        #endregion

    }

}