using UnityEngine;
using UnityEngine.InputSystem;

namespace AChildsCourage.RoomEditor
{

    public class MouseListenerEntity : MonoBehaviour
    {

        #region Constants

        public const string LeftButtonName = "leftButton";
        private const string RightButtonName = "rightButton";

        #endregion

        #region Fields

        public MouseDownEvent onMouseDown;


        [SerializeField] private Camera cam;


        private RoomEditorInput input;

        #endregion

        #region Methods

        private void Awake()
        {
            input = new RoomEditorInput();

            input.Mouse.Place.started += _ => UpdateTilePosition(LeftButtonName);
            input.Mouse.Delete.started += _ => UpdateTilePosition(RightButtonName);
            input.Mouse.Move.performed += _ => OnMouseMoved();

            input.Enable();
        }

        private void OnMouseMoved()
        {
            if (input.Mouse.Place.phase == InputActionPhase.Started) UpdateTilePosition(LeftButtonName);
            if (input.Mouse.Delete.phase == InputActionPhase.Started) UpdateTilePosition(RightButtonName);
        }

        private void UpdateTilePosition(string buttonName)
        {
            var position = GetCurrentMousePosition();

            if (MouseIsOverGameView()) onMouseDown.Invoke(new MouseDownEventArgs(position, buttonName));
        }

        private Vector2Int GetCurrentMousePosition()
        {
            var pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            return new Vector2Int(
                                  Mathf.FloorToInt(pos.x),
                                  Mathf.FloorToInt(pos.y));
        }

        private bool MouseIsOverGameView()
        {
            var mousePos = Mouse.current.position.ReadValue();
            var view = cam.ScreenToViewportPoint(mousePos);

            return !(view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1);
        }

        #endregion

    }

}