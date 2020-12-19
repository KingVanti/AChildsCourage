using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    [Serializable]
    internal class TileCollection
    {

        #region Methods

        internal Tile GetTile(CreateRng createRng)
        {
            var getVariant = createRng.Prob(_variantProb);

            return getVariant ? _variants.GetRandom(createRng) : _baseTile;
        }

        #endregion

        #region Fields



        [SerializeField] [Range(0, 100)] private float _variantProb;
        [SerializeField] private Tile _baseTile;
        [SerializeField] private Tile[] _variants;



        #endregion

    }

}