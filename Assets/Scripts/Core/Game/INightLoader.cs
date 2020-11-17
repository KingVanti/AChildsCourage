using AChildsCourage.Game.Persistance;
using Appccelerate.EventBroker;
using System;

namespace AChildsCourage.Game
{

    internal interface INightLoader
    {

        #region Events

        [EventPublication(nameof(OnFloorBuilt))]
        event EventHandler<FloorBuiltEventArgs> OnFloorBuilt;

        #endregion

        #region Methods

        void Load(NightData nightData);

        #endregion

    }

}