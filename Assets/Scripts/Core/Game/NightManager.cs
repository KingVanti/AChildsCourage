using AChildsCourage.Game.Floors.RoomPersistance;
using  static  AChildsCourage.Game.MNightPreparation;

namespace AChildsCourage.Game
{

    [Singleton]
    internal class NightManager : INightManager
    {

        #region Methods

        public void PrepareNight()
        {
            loadRunData()
                .Map(d => d.CurrentNight)
                .Do(prepareNight.Invoke);
        }

        #endregion

        #region Fields

        private readonly LoadRunData loadRunData;
        private readonly PrepareNight prepareNight;

        #endregion

        #region Constructors

        public NightManager(LoadRunData loadRunData, LoadItemIds loadItemIds, LoadRoomData loadRoom, IFloorRecreator floorRecreator)
        {
            this.loadRunData = loadRunData;

            prepareNight = Make(loadRoom, loadItemIds, floorRecreator);
        }

        public NightManager(LoadRunData loadRunData, PrepareNight prepareNight)
        {
            this.loadRunData = loadRunData;
            this.prepareNight = prepareNight;
        }

        #endregion

    }

}