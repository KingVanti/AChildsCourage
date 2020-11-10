using System.Collections;
using System.Collections.Generic;

namespace AChildsCourage.Game.Floors.Generation
{

    public class FilteredRoomPassages : IEnumerable<RoomPassages>
    {

        #region Fields

        private readonly IEnumerable<RoomPassages> passages;

        #endregion

        #region Constructors

        internal FilteredRoomPassages(IEnumerable<RoomPassages> passages)
        {
            this.passages = passages;
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