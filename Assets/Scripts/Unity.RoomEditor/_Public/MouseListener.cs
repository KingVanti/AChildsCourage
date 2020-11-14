using UnityEngine;
using UnityEngine.InputSystem;

namespace AChildsCourage.RoomEditor
{

    public class MouseListener : MonoBehaviour
    {

        #region Constants

        public const string LeftButtonName = "leftButton";
        public const string RightButtonName = "rightButton";

        #endregion

        #region Fields

        public MouseDownEvent onMouseDown;

#pragma warning disable 649

        [SerializeField] private Camera cam;

#pragma warning restore 649

        private RoomEditorInput input;

        #endregion

        #region Methods

        private void Awake()
        {
            input = new RoomEditorInput();

            input.Mouse.Place.started += (_) => UpdateTilePosition(LeftButtonName);
            input.Mouse.Delete.started += (_) => UpdateTilePosition(RightButtonName);
            input.Mouse.Move.performed += (_) => OnMouseMoved();

            input.Enable();
        }

        private void OnMouseMoved()
        {
            if (input.Mouse.Place.phase == InputActionPhase.Started)
                UpdateTilePosition(LeftButtonName);
            if (input.Mouse.Delete.phase == InputActionPhase.Started)
                UpdateTilePosition(RightButtonName);
        }

        private void UpdateTilePosition(string buttonName)
        {
            var position = GetCurrentMousePosition();

            onMouseDown.Invoke(new MouseDownEventArgs(position, buttonName));
        }

        private Vector2Int GetCurrentMousePosition()
        {
            var pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            return new Vector2Int(
                Mathf.FloorToInt(pos.x),
                Mathf.FloorToInt(pos.y));
        }

        #endregion

    }

}