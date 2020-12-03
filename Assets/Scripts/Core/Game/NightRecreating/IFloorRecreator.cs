using static AChildsCourage.Game.Floors.MFloor;

namespace AChildsCourage.Game
{

    public interface IFloorRecreator
    {

        #region Methods

        void Recreate(Floor floor);

        #endregion

    }

}