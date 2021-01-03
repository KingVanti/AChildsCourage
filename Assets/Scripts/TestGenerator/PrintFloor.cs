using AChildsCourage.Game.Floors.Courage;
using UnityEngine;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.Gen
{

    internal static class PrintFloor
    {

        private static readonly Color backGroundColor = Color.clear;
        private static readonly Color groundColor = new Color(0.51f, 0.51f, 0.51f);
        private static readonly Color wallColor = new Color(0.78f, 0.78f, 0.78f);
        private static readonly Color staticObjectColor = new Color(0.43f, 0.1f, 0.14f);
        private static readonly Color runeColor = new Color(0.36f, 0.95f, 0.97f);
        private static readonly Color sparkColor = new Color(0.97f, 0.95f, 0.07f);
        private static readonly Color orbColor = new Color(0.97f, 0.65f, 0.26f);


        internal static Texture2D PrintToTexture(this Floor floor)
        {
            var texture = CreateFittingTexture(floor);
            FillTexture(texture, backGroundColor);
            PrintFloorTo(floor, texture);
            return texture;
        }

        private static Texture2D CreateFittingTexture(Floor floor)
        {
            var dimensions = floor.Map(GetFloorDimensions);
            return new Texture2D(dimensions.Width, dimensions.Height)
            {
                filterMode = FilterMode.Point
            };
        }

        private static void FillTexture(Texture2D texture, Color color)
        {
            for (var x = 0; x < texture.width; x++)
                for (var y = 0; y < texture.height; y++)
                    texture.SetPixel(x, y, color);
        }

        private static void PrintFloorTo(Floor floor, Texture2D texture)
        {
            var print = CreatePrinter(floor, texture);

            foreach (var groundTile in floor.GroundPositions) PrintGround(groundTile, print);
            foreach (var staticObject in floor.StaticObjects) PrintStaticObject(staticObject, print);
            foreach (var rune in floor.Runes) PrintRune(rune, print);
            foreach (var couragePickup in floor.CouragePickups) PrintCouragePickup(couragePickup, print);
            foreach (var wall in floor.Walls) PrintWall(wall, print);

            texture.Apply();
        }

        private static SetTexturePixel CreatePrinter(Floor floor, Texture2D texture)
        {
            var offset = floor
                         .Map(GetFloorDimensions)
                         .LowerRight
                         .Map(AsOffset)
                         .Map(Absolute);

            return (pos, col) => texture.SetPixel(pos.X + offset.X, pos.Y + offset.Y, col);
        }

        private static void PrintGround(TilePosition groundPosition, SetTexturePixel setTexturePixel) =>
            setTexturePixel(groundPosition, groundColor);

        private static void PrintWall(Wall wall, SetTexturePixel setTexturePixel) =>
            setTexturePixel(wall.Position, wallColor);

        private static void PrintStaticObject(StaticObject staticObject, SetTexturePixel setTexturePixel) =>
            setTexturePixel(staticObject.Position, staticObjectColor);

        private static void PrintRune(Rune rune, SetTexturePixel setTexturePixel) =>
            Grid.Generate(-1, -1, 3, 3)
                .ForEach(offset => setTexturePixel(rune.Position.Map(OffsetBy, new TileOffset(offset.X, offset.Y)), runeColor));

        private static void PrintCouragePickup(CouragePickup couragePickup, SetTexturePixel setTexturePixel) =>
            setTexturePixel(couragePickup.Position, couragePickup.Variant == CourageVariant.Orb ? orbColor : sparkColor);


        private delegate void SetTexturePixel(TilePosition position, Color color);

    }

}