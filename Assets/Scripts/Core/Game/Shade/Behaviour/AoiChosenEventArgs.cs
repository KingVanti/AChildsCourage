using System;

namespace AChildsCourage.Game.Shade
{

    public class AoiChosenEventArgs : EventArgs
    {

        internal Aoi Aoi { get; }


        internal AoiChosenEventArgs(Aoi aoi) =>
            Aoi = aoi;

    }

}