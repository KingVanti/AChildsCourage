namespace AChildsCourage.Game.FloorGeneration.Persistance.Editor
{


    public interface IUpdatableRoomRepository : IRoomRepository
    {

        #region Methods

        void Update(Room room, int id);

        #endregion

    }

}