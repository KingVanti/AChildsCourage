using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Persistance;
using System;

namespace AChildsCourage.Game
{

    [Singleton]
    internal class NightLoader : INightLoader
    {

        #region Events

        public event EventHandler<FloorBuiltEventArgs> OnFloorBuilt;

        #endregion

        #region Fields

        private readonly GenerateFloor floorGenerator;
        private readonly BuildRoomTiles floorBuilder;

        #endregion

        #region Constructors

        public NightLoader(GenerateFloor floorGenerator, BuildRoomTiles floorBuilder)
        {
            this.floorGenerator = floorGenerator;
            this.floorBuilder = floorBuilder;
        }

        #endregion

        #region Methods

        public void Load(NightData nightData)
        {
            var floor = BuildFloor(nightData.Seed);

            OnFloorBuilt.Invoke(this, new FloorBuiltEventArgs(floor));
        }

        private FloorTiles BuildFloor(int seed)
        {
            var floorPlan = floorGenerator(seed);

            return floorBuilder(floorPlan);
        }

        #endregion

    }

}