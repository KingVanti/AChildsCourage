using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    public class TilePositions : IEnumerable<TilePosition>
    {

        #region Properties

        public TilePosition[] Positions { get; set; }

        #endregion

        #region Constructors

        public TilePositions(IEnumerable<TilePosition> positions)
        {
            Positions = positions.ToArray();
        }

        #endregion

        #region Methods

        public IEnumerator<TilePosition> GetEnumerator()
        {
            return Positions.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Positions.GetEnumerator();
        }

        #endregion

    }

}