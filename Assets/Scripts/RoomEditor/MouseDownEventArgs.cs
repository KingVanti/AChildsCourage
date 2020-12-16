using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class MouseDownEventArgs
    {

        #region Constructors

        public MouseDownEventArgs(Vector2Int position, string mouseButtonName)
        {
            Position = position;
            MouseButtonName = mouseButtonName;
        }

        #endregion

        #region Properties

        public Vector2Int Position { get; }

        public string MouseButtonName { get; }

        #endregion

    }

}