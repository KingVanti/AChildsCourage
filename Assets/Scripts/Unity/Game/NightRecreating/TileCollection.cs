using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.Rng;

namespace AChildsCourage.Game
{

    [Serializable]
    internal class TileCollection
    {

        #region Methods

        internal Tile GetTile(CreateRng createRng)
        {
            var getVariant = createRng.Prob(_variantProb);

            if (getVariant)
                return _variants.GetRandom(createRng);
            return _baseTile;
        }

        #endregion

        #region Fields

#pragma warning disable 649

        [SerializeField] [Range(0, 100)] private float _variantProb;
        [SerializeField] private Tile _baseTile;
        [SerializeField] private Tile[] _variants;

#pragma warning restore 649

        #endregion

    }

}