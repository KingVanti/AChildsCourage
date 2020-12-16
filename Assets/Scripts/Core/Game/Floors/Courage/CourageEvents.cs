using System;
using UnityEngine.Events;

namespace AChildsCourage.Game.Courage
{

    public static class CourageEvents
    {

        [Serializable]
        public class CourageChanged : UnityEvent<int, int, int> { }

    }

}