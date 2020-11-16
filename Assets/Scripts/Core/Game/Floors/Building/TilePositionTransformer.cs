namespace AChildsCourage.Game.Floors.Building
{

    internal class TilePositionTransformer
    {

        #region Fields

        private readonly TileOffset tileOffset;

        #endregion

        #region Constructors

        internal TilePositionTransformer(TileOffset tileOffset)
        {
            this.tileOffset = tileOffset;
        }

        #endregion

        #region Methods

        internal TilePosition Transform(TilePosition tilePosition)
        {
            return tilePosition + tileOffset;
        }

        #endregion

    }

}