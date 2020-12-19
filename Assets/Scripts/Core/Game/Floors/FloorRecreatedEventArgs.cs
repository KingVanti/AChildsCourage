using System;
using static AChildsCourage.Game.Floors.MFloor;

namespace AChildsCourage.Game.Floors
{

    public class FloorRecreatedEventArgs : EventArgs
    {

        public Floor Floor { get; }


        public FloorRecreatedEventArgs(Floor floor) => Floor = floor;

    }

}