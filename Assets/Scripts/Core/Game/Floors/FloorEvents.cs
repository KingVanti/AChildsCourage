using System;
using UnityEngine.Events;

namespace AChildsCourage.Game.Floors
{

    public static class FloorEvents
    {

        [Serializable]
        public class Floor : UnityEvent<MFloor.Floor> { }

    }

}