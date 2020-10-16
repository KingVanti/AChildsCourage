namespace AChildsCourage.Game.Floors.Generation
{

    public interface ITilePlacer 
    {

        #region Methods

        void Place(TileType type, TilePosition position);

        #endregion

    }

}