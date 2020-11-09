using AChildsCourage.Game.Floors.Generation;

namespace AChildsCourage.Game.Floors.Persistance
{

    public interface IRoomRepository
    {

        #region Methods

        FloorRooms LoadRoomsFor(FloorPlan floorPlan);

        #endregion

    }

}