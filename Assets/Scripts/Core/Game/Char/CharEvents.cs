using System;
using AChildsCourage.Game.Floors.Courage;
using UnityEngine.Events;

namespace AChildsCourage.Game.Char
{

    public static class CharEvents
    {

        [Serializable]
        public class CouragePickUp : UnityEvent<CouragePickupEntity> { }

        [Serializable]
        public class MovementState : UnityEvent<Char.MovementState> { }

    }

}