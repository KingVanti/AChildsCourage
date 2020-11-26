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

        internal static Vector3 ToVector3(this TilePosition tilePosition)
        {
            return new Vector3(
                tilePosition.X,
                tilePosition.Y,
                0);
        }

        #endregion

    }

}