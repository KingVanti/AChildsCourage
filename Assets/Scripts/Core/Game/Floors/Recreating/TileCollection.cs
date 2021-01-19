using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using static AChildsCourage.Rng;

namespace AChildsCourage.Game
{

    [Serializable]
    internal class TileCollection
    {

        [SerializeField] [Range(0, 100)] private float variantProb;
        [SerializeField] private Tile baseTile;
        [SerializeField] private Tile[] variants;

        internal Tile GetTile(Rng rng)
        {
            var getVariant = rng.Map(Prob, variantProb);

            return getVariant ? variants.TryGetRandom(rng, () => baseTile) : baseTile;
        }

    }

}