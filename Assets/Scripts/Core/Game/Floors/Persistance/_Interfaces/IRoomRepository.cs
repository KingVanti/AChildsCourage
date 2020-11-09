namespace AChildsCourage.Game.Floors.Persistance
{

    public interface IRoomRepository
    {

        #region Methods

        RoomData Load(int id);

        #endregion

    }

}