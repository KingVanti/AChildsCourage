using System.Numerics;

namespace AChildsCourage.Game.NightLoading
{

    internal static partial class FloorGenerating
    {

        internal static float GetDistanceFromOrigin(TilePosition position)
        {
            return (float)new Vector2(position.X, position.Y).Length();
        }


        internal static float GetDistanceBetween(TilePosition p1, TilePosition p2)
        {
            return Vector2.Distance(
                new Vector2(p1.X, p1.Y),
                new Vector2(p2.X, p2.Y));
        }

    }

}