using AChildsCourage.Game.Floors.Persistance;

namespace AChildsCourage.Game.Floors.Generation
{

    public interface IRoomGenerator
    {

        #region Methods

        Room GenerateFrom(RoomData data);

        #endregion

    }

}