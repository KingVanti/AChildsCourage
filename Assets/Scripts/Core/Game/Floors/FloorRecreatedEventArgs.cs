using System;

namespace AChildsCourage.Game.Floors
{

    public class FloorRecreatedEventArgs : EventArgs
    {

        internal Floor Floor { get; }


        internal FloorRecreatedEventArgs(Floor floor) =>
            Floor = floor;

    }

}