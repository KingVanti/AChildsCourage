using AChildsCourage.Game.Floors.Generation;
using System;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Persistance
{

    [Serializable]
    public class SerializablePassages
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private bool hasNorth;
        [SerializeField] private bool hasEast;
        [SerializeField] private bool hasSouth;
        [SerializeField] private bool hasWest;

#pragma warning restore 649

        #endregion

        #region Constructors

        public SerializablePassages(ChunkPassages passages)
        {
            hasNorth = passages.HasNorth;
            hasEast = passages.HasEast;
            hasSouth = passages.HasSouth;
            hasWest = passages.HasWest;
        }

        #endregion

        #region Methods

        public ChunkPassages Deserialize()
        {
            return new ChunkPassages(hasNorth, hasEast, hasSouth, hasWest);
        }

        #endregion

    }

}