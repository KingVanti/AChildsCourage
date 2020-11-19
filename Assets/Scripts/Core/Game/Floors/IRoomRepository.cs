using AChildsCourage.Game.NightManagement.Loading;

namespace AChildsCourage.Game.Floors
{

    public interface IRoomRepository
    {

        #region Methods

        RoomsForFloor LoadRoomsFor(FloorPlan floorPlan);

        #endregion

    }

}