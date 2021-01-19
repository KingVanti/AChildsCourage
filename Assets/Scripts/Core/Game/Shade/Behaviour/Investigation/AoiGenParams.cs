using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    [Serializable]
    public struct AoiGenParams
    {

        [SerializeField] private int poiCount;
        [SerializeField] private float radius;
        [SerializeField] private float poiDistance;


        public int PoiCount => poiCount;

        public float Radius => radius;

        public float PoiDistance => poiDistance;

    }

}