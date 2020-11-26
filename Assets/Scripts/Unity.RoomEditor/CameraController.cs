using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class CameraController : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private float speed;

#pragma warning restore 649

        private Vector2 currentMovement;
        private RoomEditorInput input;

        #endregion

        #region Methods

        private void Awake()
        {
            input = new RoomEditorInput();

            input.Movement.Horizontal.started += c => StartHorizontalMovement(c.ReadValue<float>());
            input.Movement.Horizontal.canceled += _ => StopHorizontalMovement();

            input.Movement.Vertical.started += c => StartVerticalMovement(c.ReadValue<float>());
            input.Movement.Vertical.canceled += _ => StopVerticalMovement();

            input.Enable();
        }

        private void StartHorizontalMovement(float direction)
        {
            currentMovement.x = direction * speed;
        }

        private void StopHorizontalMovement()
        {
            currentMovement.x = 0;
        }

        private void StartVerticalMovement(float direction)
        {
            currentMovement.y = direction * speed;
        }

        private void StopVerticalMovement()
        {
            currentMovement.y = 0;
        }

        private void Update()
        {
            transform.position += (Vector3)currentMovement * Time.deltaTime;
        }

        #endregion

    }

}