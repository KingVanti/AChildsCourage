using System;
using System.Collections;
using System.Collections.Generic;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public class FloorAOIs : IEnumerable<AOI>
    {

        public IEnumerator<AOI> GetEnumerator() => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

}