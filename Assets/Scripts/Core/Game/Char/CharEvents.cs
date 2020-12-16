using System;
using AChildsCourage.Game.Courage;
using AChildsCourage.Game.Items;
using UnityEngine.Events;

namespace AChildsCourage.Game.Char
{

    public static class CharEvents
    {
        
        [Serializable]
        public class Cooldown : UnityEvent<int, float, float> { }

        [Serializable]
        public class ItemData : UnityEvent<Items.ItemData> { }
        
        [Serializable]
        public class PickUp : UnityEvent<int, int> { }

        [Serializable]
        public class CouragePickUp : UnityEvent<CouragePickupEntity> { }
        
    }

}