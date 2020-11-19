using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AChildsCourage.Game.Floors
{

    [Serializable]
    internal class TileCollection
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] [Range(0, 100)] private float _variantProb;
        [SerializeField] private Tile _baseTile;
        [SerializeField] private Tile[] _variants;

#pragma warning restore 649

        #endregion

        #region Methods

        internal Tile GetTile(IRNG rng)
        {
            var getVariant = rng.Prob(_variantProb);

            if (getVariant)
                return _variants.GetRandom(rng);
            else
                return _baseTile;
        }

        #endregion

    }

}