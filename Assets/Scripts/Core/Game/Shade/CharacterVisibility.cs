using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Shade
{

    public static class MVisibility
    {

        public static Func<IEnumerable<Visibility>, Visibility> GetHighestValue =>
            visibilities =>
                visibilities
                    .OrderByDescending(GetVisibilityValue)
                    .First();

        private static Func<Visibility, int> GetVisibilityValue =>
            visibility => (int) visibility;


        public enum Visibility
        {

            NotVisible,
            Secondary,
            Primary

        }

    }

}