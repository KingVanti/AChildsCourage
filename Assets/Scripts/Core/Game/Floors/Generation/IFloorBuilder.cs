using Appccelerate.EventBroker;
using System;

namespace AChildsCourage.Game.Floors.Generation
{

    internal interface IFloorBuilder
    {

        #region Events

        [EventPublication(nameof(OnFloorBuilt))]
        event EventHandler<FloorBuiltEventArgs> OnFloorBuilt;

        #endregion

        #region Methods

        void Build(FloorPlan floorPlan);

        #endregion

    }

}