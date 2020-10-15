using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [Serializable]
    internal class SerializableTilePositions
    {

        #region Static Methods

        internal static SerializableTilePositions From(TilePositions tilePositions)
        {
            var positions = tilePositions.Select(SerializableTilePosition.From).ToArray();

            return new SerializableTilePositions(positions);
        }

        #endregion

        #region Fields

        [SerializeField] internal SerializableTilePosition[] positions;

        #endregion

        #region Constructors

        public SerializableTilePositions(SerializableTilePosition[] positions)
        {
            this.positions = positions;
        }

        #endregion

        #region Methods

        internal TilePositions ToTilePositions()
        {
            return new TilePositions(positions.Select(p => p.ToTilePosition()));
        }


        public override bool Equals(object obj)
        {
            return obj is SerializableTilePositions other &&
                  positions.SequenceEqual(other.positions);
        }


        public override int GetHashCode()
        {
            return -1378504013 + EqualityComparer<SerializableTilePosition[]>.Default.GetHashCode(positions);
        }

        #endregion

    }

}