using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public class FilteredRoomPassages : IEnumerable<RoomPassages>
    {

        #region Fields

        private readonly IEnumerable<RoomPassages> passages;

        #endregion

        #region Constructors

        public FilteredRoomPassages(IEnumerable<RoomPassages> passages)
        {
            var passageArray = passages.ToArray();
            
            if (passageArray.Length > 0)
                this.passages = passageArray;
            else
                throw new Exception("No passages in this filter");
        }

        #endregion

        #region Methods

        public IEnumerator<RoomPassages> GetEnumerator() => passages.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => passages.GetEnumerator();

        #endregion

    }

}