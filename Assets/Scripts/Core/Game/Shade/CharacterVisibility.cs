using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Shade
{

    public static class MVisibility
    {

        public static Visibility GetHighestValue(IEnumerable<Visibility> visibilities) =>
            visibilities
                .OrderByDescending(GetVisibilityValue)
                .First();

        private static int GetVisibilityValue(Visibility visibility) =>
            (int) visibility;


        public enum Visibility
        {

            NotVisible,
            Secondary,
            Primary

        }

    }

}