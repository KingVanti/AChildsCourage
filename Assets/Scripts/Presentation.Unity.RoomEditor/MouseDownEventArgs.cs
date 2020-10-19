using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class MouseDownEventArgs
    {

        #region Properties

        public Vector2Int Position { get; }

        public string MouseButtonName { get; }

        #endregion

        #region Constructors

        public MouseDownEventArgs(Vector2Int position, string mouseButtonName)
        {
            Position = position;
            MouseButtonName = mouseButtonName;
        }

        #endregion

    }

}