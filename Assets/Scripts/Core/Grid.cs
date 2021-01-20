using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game;

namespace AChildsCourage
{

    internal static class Grid
    {

        internal static IEnumerable<(int X, int Y)> Generate(int originX, int originY, int width, int height)
        {
            for (var dx = 0; dx < width; dx++)
                for (var dy = 0; dy < height; dy++)
                    yield return (originX + dx, originY + dy);
        }

        internal static IEnumerable<TilePosition> GenerateTiles(int originX, int originY, int width, int height) =>
            Generate(originX, originY, width, height)
                .Select(p => new TilePosition(p.X, p.Y));

    }

}