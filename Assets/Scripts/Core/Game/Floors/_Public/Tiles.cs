using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors
{

    public class Tiles<TTile> : IEnumerable<TTile>
    {

        #region Static Properties

        public static Tiles<TTile> None { get { return new Tiles<TTile>(); } }

        #endregion

        #region Fields

        private readonly TTile[] tiles;

        #endregion

        #region Constructors

        public Tiles(IEnumerable<TTile> tiles)
        {
            this.tiles = tiles.ToArray();
        }

        internal Tiles(params TTile[] tiles)
        {
            this.tiles = tiles;
        }

        #endregion

        #region Methods

        public IEnumerator<TTile> GetEnumerator()
        {
            return tiles.Cast<TTile>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return tiles.GetEnumerator();
        }

        #endregion

    }

}