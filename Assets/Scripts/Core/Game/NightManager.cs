using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Items;
using static AChildsCourage.Game.MNightRecreating;
using static AChildsCourage.Game.Persistence.MRunData;
using static AChildsCourage.Game.FloorGenerating;
using static AChildsCourage.Game.MFloorPlanGenerating;

namespace AChildsCourage.Game
{

    [Singleton]
    internal class NightManager : INightManager
    {

        #region Constructors

        public NightManager(LoadRunData loadRunData, LoadItemIds loadItemIds, LoadRoomData loadRoomData, IFloorRecreator floorRecreator)
        {
            this.loadRunData = loadRunData;
            roomData = loadRoomData().ToArray();
            itemIds = loadItemIds().ToArray();
            recreateNight = Make(floorRecreator);

            floorPlanGenerationParameters = new GenerationParameters(
                roomData.SelectMany(d => d.GetPassageVariations()).ToArray());
        }

        #endregion

        #region Methods

        public void PrepareNightForCurrentRun()
        {
            var nightData = loadRunData()
                .Map(runData => StartNight(runData, MRng.Random()));

            var rng = MRng.FromSeed(nightData.Seed);

            GenerateFloorPlan(floorPlanGenerationParameters, rng)
                .Map(floorPlan => GenerateFloor(floorPlan, itemIds, roomData, rng))
                .Do(recreateNight.Invoke);
        }

        #endregion

        #region Fields

        private readonly GenerationParameters floorPlanGenerationParameters;
        private readonly LoadRunData loadRunData;
        private readonly RoomData[] roomData;
        private readonly ItemId[] itemIds;
        private readonly RecreateNight recreateNight;

        #endregion

    }

}