using System;
using UnityEngine.Events;

namespace AChildsCourage
{

    public static class Events
    {

        [Serializable]
        public class Bool : UnityEvent<bool> { }
        
        [Serializable]
        public class Int : UnityEvent<int> { }

        [Serializable]
        public class Float : UnityEvent<float> { }

        [Serializable]
        public class Vector3 : UnityEvent<UnityEngine.Vector3> { }
        
        [Serializable]
        public class Vector2 : UnityEvent<UnityEngine.Vector2> { }

        [Serializable]
        public class Empty : UnityEvent { }

    }

}