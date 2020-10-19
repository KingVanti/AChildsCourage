using AChildsCourage.Game;

namespace AChildsCourage.RoomEditor
{

    public class MouseDownEventArgs
    {

        #region Properties

        public TilePosition Position { get; }

        public string MouseButtonName { get; }

        #endregion

        #region Constructors

        public MouseDownEventArgs(TilePosition position, string mouseButtonName)
        {
            Position = position;
            MouseButtonName = mouseButtonName;
        }

        #endregion

    }

}