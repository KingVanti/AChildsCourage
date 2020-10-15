namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    public interface IRoomRepository
    {

        #region Methods

        Room Load(int id);

        bool Contains(int id);

        #endregion

    }

}