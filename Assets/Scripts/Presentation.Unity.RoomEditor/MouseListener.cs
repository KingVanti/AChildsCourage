using AChildsCourage.Game;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace AChildsCourage.RoomEditor
{

    public class MouseListener : MonoBehaviour
    {

        #region Fields

        public MouseDownEvent onMouseDown;

#pragma warning disable 649

        [SerializeField] private Camera cam;

#pragma warning restore 649

        private string lastButtonName;
        private TilePosition lastTilePosition;
        private RoomEditorInput input;

        #endregion

        #region Properties

        #endregion

        #region Methods

        private void Awake()
        {
            input = new RoomEditorInput();

            input.Mouse.Place.started += (_) => UpdateTilePosition("leftButton");
            input.Mouse.Delete.started += (_) => UpdateTilePosition("rightButton");
            input.Mouse.Move.performed += (_) => OnMouseMoved();

            input.Enable();
        }

        private void OnMouseMoved()
        {
            if (input.Mouse.Place.phase == InputActionPhase.Started)
                UpdateTilePosition("leftButton");
            if (input.Mouse.Delete.phase == InputActionPhase.Started)
                UpdateTilePosition("rightButton");
        }

        private void UpdateTilePosition(string buttonName)
        {
            var position = GetCurrentMousePosition();

            if (!position.Equals(lastTilePosition) || buttonName != lastButtonName)
            {
                lastTilePosition = position;
                lastButtonName = buttonName;
                onMouseDown.Invoke(new MouseDownEventArgs(position, buttonName));
            }
        }

        private TilePosition GetCurrentMousePosition()
        {
            var pos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            return new TilePosition(
                Mathf.FloorToInt(pos.x),
                Mathf.FloorToInt(pos.y));
        }

        #endregion

    }

}