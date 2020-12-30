using AChildsCourage.Game.Floors.Courage;
using UnityEngine;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game.Floors.TestGenerator
{

    internal static class GenerateTexture
    {

        private static readonly Color backGroundColor = Color.clear;
        private static readonly Color groundTileColor = new Color(0.51f, 0.51f, 0.51f);
        private static readonly Color staticObjectColor = new Color(0.43f, 0.1f, 0.14f);
        private static readonly Color runeColor = new Color(0.36f, 0.95f, 0.97f);
        private static readonly Color sparkColor = new Color(0.97f, 0.95f, 0.07f);
        private static readonly Color orbColor = new Color(0.97f, 0.65f, 0.26f);

        internal static Texture2D From(Floor floor)
        {
            var texture = CreateFittingTexture(floor);
            FillTexture(texture, backGroundColor);
            PrintFloorToTexture(floor, texture);
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

        private static void PrintFloorToTexture(Floor floor, Texture2D texture)
        {
            var print = CreatePrinter(floor, texture);

            foreach (var room in floor.Rooms) PrintRoom(room, print);
            foreach (var rune in floor.Runes) PrintRune(rune, print);
            foreach (var couragePickup in floor.CouragePickups) PrintCouragePickup(couragePickup, print);

            texture.Apply();
        }

        private static PrintToTexture CreatePrinter(Floor floor, Texture2D texture)
        {
            var offset = floor.Map(GetFloorDimensions).LowerRight.Map(AsOffset);

            return (pos, col) => texture.SetPixel(pos.X - offset.X, pos.Y - offset.Y, col);
        }

        private static void PrintRoom(Room room, PrintToTexture print)
        {
            foreach (var groundTile in room.GroundTiles) PrintGroundTile(groundTile, print);
            foreach (var staticObject in room.StaticObjects) PrintStaticObject(staticObject, print);
        }

        private static void PrintGroundTile(GroundTile groundTile, PrintToTexture print) =>
            print(groundTile.Position, groundTileColor);

        private static void PrintStaticObject(StaticObject staticObject, PrintToTexture print) =>
            print(staticObject.Position, staticObjectColor);

        private static void PrintRune(Rune rune, PrintToTexture print)
        {
            for (var dX = -1; dX <= 1; dX++)
                for (var dY = -1; dY <= 1; dY++)
                    print(rune.Position.Map(OffsetBy, new TileOffset(dX, dY)), runeColor);
        }

        private static void PrintCouragePickup(CouragePickup couragePickup, PrintToTexture print) =>
            print(couragePickup.Position, couragePickup.Variant == CourageVariant.Orb ? orbColor : sparkColor);


        private delegate void PrintToTexture(TilePosition position, Color color);

    }

}