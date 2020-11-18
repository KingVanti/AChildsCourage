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
            if (passages.Count() > 0)
                this.passages = passages;
            else
                throw new System.Exception("No passages in this filter");
        }

        #endregion

        #region Methods

        public IEnumerator<RoomPassages> GetEnumerator()
        {
            return passages.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return passages.GetEnumerator();
        }

        #endregion

    }

}