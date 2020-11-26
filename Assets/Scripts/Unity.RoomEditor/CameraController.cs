using AChildsCourage.Game.NightLoading;
using ICSharpCode.NRefactory.Visitors;
using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class CameraController : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private float speed;
        [SerializeField] private float zoomSpeed;
        [SerializeField] private float minZoom;
        [SerializeField] private float maxZoom;
        [SerializeField] private new Camera camera;

#pragma warning restore 649

        private Vector2 currentMovement;
        private RoomEditorInput input;

        #endregion

        #region Properties

        private float Zoom { get { return camera.orthographicSize; } set { camera.orthographicSize = Mathf.Clamp(value, minZoom, maxZoom); } }

        #endregion

        #region Methods

        private void Awake()
        {
            input = new RoomEditorInput();

            input.Movement.Horizontal.started += c => StartHorizontalMovement(c.ReadValue<float>());
            input.Movement.Horizontal.canceled += _ => StopHorizontalMovement();

            input.Movement.Vertical.started += c => StartVerticalMovement(c.ReadValue<float>());
            input.Movement.Vertical.canceled += _ => StopVerticalMovement();

            input.Zoom.Scroll.performed += c => OnScroll(c.ReadValue<float>());

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

        private void OnScroll(float delta)
        {
            var normalized = delta / Mathf.Abs(delta);

            Zoom += -normalized * zoomSpeed;
        }

        private void Update()
        {
            transform.position += (Vector3)currentMovement * Time.deltaTime;
        }

        #endregion

    }

}