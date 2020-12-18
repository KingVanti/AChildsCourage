using System;
using UnityEngine.Events;

namespace AChildsCourage.Game.Floors.Courage
{

    public static class CourageEvents
    {

        [Serializable]
        public class CourageChanged : UnityEvent<int, int> { }

    }

}