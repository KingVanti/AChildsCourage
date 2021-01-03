using System.Collections.Generic;

namespace AChildsCourage
{

    public static class Grid
    {
        public static IEnumerable<(int X, int Y)> Generate(int originX, int originY, int width, int height)
        {
            for(var dx = 0; dx < width;dx++)
                for (var dy = 0; dy < height; dy++)
                    yield return (originX + dx, originY + dy);
        }
    }

}