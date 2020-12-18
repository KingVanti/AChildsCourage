using System;
using UnityEngine.Events;

namespace AChildsCourage
{

    public static class Events
    {

        [Serializable]
        public class Bool : UnityEvent<bool> { }

        [Serializable]
        public class Empty : UnityEvent { }

    }

}