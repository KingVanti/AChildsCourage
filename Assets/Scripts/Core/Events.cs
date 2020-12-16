using System;
using UnityEngine.Events;

namespace AChildsCourage
{

    public static class Events
    {

        [Serializable]
        public class Bool : UnityEvent<bool> { }

        [Serializable]
        public class Float : UnityEvent<float> { }

        [Serializable]
        public class Vector3 : UnityEvent<UnityEngine.Vector3> { }

        [Serializable]
        public class Empty : UnityEvent { }
        
    }

}