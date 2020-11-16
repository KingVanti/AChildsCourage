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

        private readonly FloorGenerator floorGenerator;
        private readonly FloorTilesBuilder floorBuilder;

        #endregion

        #region Constructors

        public NightLoader(FloorGenerator floorGenerator, FloorTilesBuilder floorBuilder)
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
            //var floorPlan = floorGenerator(seed);
            var floorPlan = new FloorPlan(new[]
            {
                new RoomPlan(0, new ChunkPosition(0,0))
            });

            return floorBuilder(floorPlan);
        }

        #endregion

    }

}