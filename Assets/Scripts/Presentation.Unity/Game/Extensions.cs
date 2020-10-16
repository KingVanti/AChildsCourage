using System;
using UnityEngine;

namespace AChildsCourage.Game
{

    public static class Extensions
    {

        #region Methods

        public static Vector3Int ToVector3Int(this TilePosition tilePosition)
        {
            return new Vector3Int(
                tilePosition.X,
                tilePosition.Y,
                0);
        }

        #endregion

    }

}