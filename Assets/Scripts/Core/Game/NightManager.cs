using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.NightLoading;

namespace AChildsCourage.Game
{

    [Singleton]
    internal class NightManager : INightManager
    {

        #region Methods

        public void PrepareNight()
        {
            _loadRunData()
                .Map(d => d.CurrentNight)
                .Do(_loadNight.Invoke);
        }

        #endregion

        #region Fields

        private readonly LoadRunData _loadRunData;
        private readonly LoadNight _loadNight;

        #endregion

        #region Constructors

        public NightManager(LoadRunData loadRunData, LoadItemIds loadItemIds, LoadRoomData loadRoom, IFloorRecreator floorRecreator)
        {
            _loadRunData = loadRunData;

            _loadNight = NightLoading.NightLoading.Make(loadRoom, loadItemIds, floorRecreator);
        }

        public NightManager(LoadRunData loadRunData, LoadNight loadNight)
        {
            _loadRunData = loadRunData;
            _loadNight = loadNight;
        }

        #endregion

    }

}