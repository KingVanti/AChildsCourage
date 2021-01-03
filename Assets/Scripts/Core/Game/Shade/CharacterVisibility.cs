using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Shade
{

    public readonly struct Visibility
    {

        public static Visibility GetHighestValue(IEnumerable<Visibility> visibilities) =>
            visibilities
                .OrderByDescending(v => v.value)
                .First();


        public static Visibility NotVisible => new Visibility(0);

        public static Visibility Secondary => new Visibility(1);

        public static Visibility Primary => new Visibility(2);


        private readonly int value;

        
        private Visibility(int value) =>
            this.value = value;

    }

}