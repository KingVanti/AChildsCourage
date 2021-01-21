using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Char
{

    [Serializable]
    public struct Visibility
    {

        public static readonly Visibility notVisible = new Visibility(0);
        public static readonly Visibility secondary = new Visibility(1);
        public static readonly Visibility primary = new Visibility(2);


        [SerializeField] private int value;


        private Visibility(int value) =>
            this.value = value;

        public static Visibility GetHighestValue(IEnumerable<Visibility> visibilities) =>
            visibilities
                .OrderByDescending(v => v.value)
                .First();

    }

}