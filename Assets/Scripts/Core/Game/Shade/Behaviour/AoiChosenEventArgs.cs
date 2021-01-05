using System;

namespace AChildsCourage.Game.Shade
{

    public class AoiChosenEventArgs : EventArgs
    {

        public Aoi Aoi { get; }


        public AoiChosenEventArgs(Aoi aoi) =>
            Aoi = aoi;

    }

}