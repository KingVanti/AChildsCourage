using AChildsCourage.Game.Floors.Persistance;
using System;

using static AChildsCourage.Game.Floors.Building.FloorBuildingModule;

namespace AChildsCourage.Game.Floors.Building
{

    [Singleton]
    internal class FloorBuilder : IFloorBuilder, IEagerActivation
    {

        #region Events

        public event EventHandler<FloorBuiltEventArgs> OnFloorBuilt;

        #endregion

        #region Fields

        private readonly IRoomRepository roomRepository;

        #endregion

        #region Constructors

        public FloorBuilder(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        #endregion

        #region Methods

        public void Build(FloorPlan floorPlan)
        {
            var floor = BuildFrom(roomRepository.LoadRoomsFor(floorPlan));

            OnFloorBuilt?.Invoke(this, new FloorBuiltEventArgs(floor));
        }

        #endregion

    }

}