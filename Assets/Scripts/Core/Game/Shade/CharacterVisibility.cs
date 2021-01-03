using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    [Serializable]
    public struct Visibility
    {

        [SerializeField] private int value;


        public static Visibility NotVisible => new Visibility(0);

        public static Visibility Secondary => new Visibility(1);

        public static Visibility Primary => new Visibility(2);


        private Visibility(int value) =>
            this.value = value;

        public static Visibility GetHighestValue(IEnumerable<Visibility> visibilities) =>
            visibilities
                .OrderByDescending(v => v.value)
                .First();

    }

}