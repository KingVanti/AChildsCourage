using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors.Persistance
{

    public class PositionList : IEnumerable<TilePosition>
    {

        #region Static Properties

        public static PositionList Empty { get { return new PositionList(Enumerable.Empty<TilePosition>()); } }

        #endregion

        #region Fields

        private readonly IEnumerable<TilePosition> positions;

        #endregion

        #region Constructors

        public PositionList(IEnumerable<TilePosition> positions)
        {
            this.positions = positions;
        }

        public PositionList(params TilePosition[] positions)
        {
            this.positions = positions;
        }

        #endregion

        #region Methods

        public IEnumerator<TilePosition> GetEnumerator()
        {
            return positions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return positions.GetEnumerator();
        }

        #endregion

    }

}