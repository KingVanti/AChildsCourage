using System.Linq;
using AChildsCourage.Game.Floors.Courage;
using UnityEngine;
using static AChildsCourage.Game.Floors.Floor;
using static AChildsCourage.Game.TilePosition;
using static AChildsCourage.Game.TileOffset;

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

            floor.Objects
                 .OrderBy(GetZIndex)
                 .ForEach(o => PrintFloorObject(o, print));

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

        private static float GetZIndex(FloorObject floorObject) =>
            floorObject.Data is GroundTileData ? 0 : 1;

        private static void PrintFloorObject(FloorObject floorObject, SetTexturePixel setTexturePixel)
        {
            switch (floorObject.Data)
            {
                case GroundTileData groundTileData:
                    PrintGround(floorObject.Position, groundTileData, setTexturePixel);
                    break;
                case WallData wallData:
                    PrintWall(floorObject.Position, wallData, setTexturePixel);
                    break;
                case StaticObjectData staticObjectData:
                    PrintStaticObject(floorObject.Position, staticObjectData, setTexturePixel);
                    break;
                case CouragePickupData couragePickupData:
                    PrintCouragePickup(floorObject.Position, couragePickupData, setTexturePixel);
                    break;
                case RuneData runeData:
                    PrintRune(floorObject.Position, runeData, setTexturePixel);
                    break;
            }
        }

        private static void PrintGround(TilePosition groundPosition, GroundTileData _, SetTexturePixel setTexturePixel) =>
            setTexturePixel(groundPosition, groundColor);

        private static void PrintWall(TilePosition position, WallData _, SetTexturePixel setTexturePixel) =>
            setTexturePixel(position, wallColor);

        private static void PrintStaticObject(TilePosition position, StaticObjectData _, SetTexturePixel setTexturePixel) =>
            setTexturePixel(position, staticObjectColor);

        private static void PrintRune(TilePosition position, RuneData _, SetTexturePixel setTexturePixel) =>
            Grid.Generate(-1, -1, 3, 3)
                .Select(t => new TileOffset(t.X, t.Y))
                .Select(ApplyTo, position)
                .ForEach(offsetPosition => setTexturePixel(offsetPosition, runeColor));

        private static void PrintCouragePickup(TilePosition position, CouragePickupData couragePickupData, SetTexturePixel setTexturePixel) =>
            setTexturePixel(position,
                            couragePickupData.Variant == CourageVariant.Orb
                                ? orbColor
                                : sparkColor);


        private delegate void SetTexturePixel(TilePosition position, Color color);

    }

}