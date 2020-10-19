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

        #endregion

        #region Properties

        #endregion

        #region Methods

        private void Update()
        {
            UpdateClick();
            UpdateDrag();
        }

        private void UpdateClick()
        {
            UpdateClickFor(Mouse.current.leftButton);
            UpdateClickFor(Mouse.current.rightButton);
        }

        private void UpdateClickFor(ButtonControl mouseButton)
        {
            if (mouseButton.wasPressedThisFrame)
                UpdateTilePosition(mouseButton.name);
        }

        private void UpdateDrag()
        {
            UpdateDragFor(Mouse.current.leftButton);
            UpdateDragFor(Mouse.current.rightButton);
        }

        private void UpdateDragFor(ButtonControl mouseButton)
        {
            if (mouseButton.isPressed)
                UpdateTilePosition(mouseButton.name);
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