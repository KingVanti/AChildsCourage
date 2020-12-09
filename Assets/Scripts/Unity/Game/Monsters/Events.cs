using System;
using UnityEngine.Events;

namespace AChildsCourage.Game.Monsters
{

    public static class ShadeEvents
    {

        [Serializable]
        public class VisibilityEvent : UnityEvent<Visibility> { }
        
        [Serializable]
        public class TilesInViewEvent : UnityEvent<TilesInView> { }

    }

}