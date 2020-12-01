using UnityEngine;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    internal static class Extensions
    {

        #region Methods

        internal static Vector3Int ToVector3Int(this TilePosition tilePosition) =>
            new Vector3Int(
                tilePosition.X,
                tilePosition.Y,
                0);

        internal static Vector3 ToVector3(this TilePosition tilePosition) =>
            new Vector3(
                tilePosition.X,
                tilePosition.Y,
                0);

        #endregion

    }

}