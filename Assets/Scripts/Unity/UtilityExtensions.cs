using System;
using UnityEngine;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage
{

    public static class UtilityExtensions
    {

        public static Vector3 ToVector(this float angle) =>
            new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad));

        public static T Log<T>(this T item)
        {
            Debug.Log(item);
            return item;
        }

        public static T Log<T>(this T item, Func<T, string> formatter)
        {
            Debug.Log(formatter(item));
            return item;
        }

        public static Vector3Int ToVector3Int(this TilePosition tilePosition) =>
            new Vector3Int(
                tilePosition.X,
                tilePosition.Y,
                0);

        public static Vector3 ToVector3(this TilePosition tilePosition) =>
            new Vector3(
                tilePosition.X,
                tilePosition.Y,
                0);
        
        public static TilePosition FloorToTile(this Vector3 position) =>
            new TilePosition(
                Mathf.FloorToInt(position.x),
                Mathf.FloorToInt(position.y));

        public static Vector3 GetTileCenter(this Vector3 position) =>
            new Vector3(
                Mathf.RoundToInt(position.x) + 0.5f,
                Mathf.RoundToInt(position.y) + 0.5f);

    }

}