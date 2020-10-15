using System;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [Serializable]
    internal class SerializableTilePosition
    {

        #region Static Methods

        internal static SerializableTilePosition From(TilePosition tilePosition)
        {
            return new SerializableTilePosition(tilePosition.X, tilePosition.Y);
        }

        #endregion

        #region Fields

        internal int x;
        internal int y;

        #endregion

        #region Constructors

        internal SerializableTilePosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Methods

        internal TilePosition ToTilePosition()
        {
            return new TilePosition(x, y);
        }


        public override bool Equals(object obj)
        {
            return obj is SerializableTilePosition position &&
                   x == position.x &&
                   y == position.y;
        }


        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        #endregion

    }

}