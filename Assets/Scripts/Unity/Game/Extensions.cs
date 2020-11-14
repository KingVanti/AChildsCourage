using UnityEngine;

namespace AChildsCourage.Game
{

    internal static class Extensions
    {

        #region Methods

        internal static Vector3Int ToVector3Int(this TilePosition tilePosition)
        {
            return new Vector3Int(
                tilePosition.X,
                tilePosition.Y,
                0);
        }

        #endregion

    }

}