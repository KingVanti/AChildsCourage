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
            var getVariant = createRng.Prob(variantProb);

            if (getVariant)
                return variants.GetRandom(createRng);
            return baseTile;
        }

        #endregion

        #region Fields

#pragma warning disable 649

        [SerializeField] [Range(0, 100)] private float variantProb;
        [SerializeField] private Tile baseTile;
        [SerializeField] private Tile[] variants;

#pragma warning restore 649

        #endregion

    }

}