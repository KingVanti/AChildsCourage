namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    public interface IRoomRepository
    {

        #region Methods

        RoomData Load(int id);

        bool Contains(int id);

        #endregion

    }

}