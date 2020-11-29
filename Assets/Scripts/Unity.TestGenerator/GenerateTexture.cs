using System;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors.TestGenerator
{

    internal static class GenerateTexture
    {

        #region Subtypes

        private enum RoomType
        {

            Start,
            Normal,
            End

        }

        #endregion

        #region Methods

        internal static Texture2D From(FloorPlan floorPlan, CompleteRoomLoader roomLoader)
        {
            var pixels = ConvertToColorArray(floorPlan, roomLoader);
            var texture = new Texture2D(pixels[0]
                                            .Length, pixels.Length) { filterMode = FilterMode.Point };

            texture.SetPixels(pixels.SelectMany(x => x)
                                    .ToArray());
            texture.Apply();

            return texture;
        }

        private static Color[][] ConvertToColorArray(FloorPlan floorPlan, CompleteRoomLoader roomLoader)
        {
            var pixels = CreateColorArray(floorPlan);
            var offset = CalculateChunkOffset(floorPlan);

            for (var i = 0; i < floorPlan.Rooms.Length; i++)
            {
                var type = RoomType.Normal;

                if (i == 0)
                    type = RoomType.Start;
                if (i == floorPlan.Rooms.Length - 1)
                    type = RoomType.End;

                PrintRoom(type, floorPlan.Rooms[i], offset, roomLoader, pixels);
            }

            return pixels;
        }

        private static Color[][] CreateColorArray(FloorPlan floorPlan)
        {
            var width = GetFloorPlanWidth(floorPlan) * 5;
            var height = GetFloorPlanHeight(floorPlan) * 5;

            var pixels = new Color[width][];

            for (var x = 0; x < width; x++)
                pixels[x] = new Color[height];

            return pixels;
        }

        private static int GetFloorPlanWidth(FloorPlan floorPlan)
        {
            var minX = floorPlan.Rooms.Select(r => r.Transform.Position.X)
                                .Min();
            var maxX = floorPlan.Rooms.Select(r => r.Transform.Position.X)
                                .Max();

            return Mathf.Abs(minX - maxX) + 1;
        }

        private static int GetFloorPlanHeight(FloorPlan floorPlan)
        {
            var minY = floorPlan.Rooms.Select(r => r.Transform.Position.Y)
                                .Min();
            var maxY = floorPlan.Rooms.Select(r => r.Transform.Position.Y)
                                .Max();

            return Mathf.Abs(minY - maxY) + 1;
        }

        private static Vector2Int CalculateChunkOffset(FloorPlan floorPlan)
        {
            return new Vector2Int(
                -floorPlan.Rooms.Select(r => r.Transform.Position.X)
                          .Min(),
                -floorPlan.Rooms.Select(r => r.Transform.Position.Y)
                          .Min());
        }

        private static void PrintRoom(RoomType type, RoomPlan room, Vector2Int offset, CompleteRoomLoader roomLoader, Color[][] pixels)
        {
            var position = GetPixelPos(room.Transform.Position, offset);
            var passages = roomLoader.GetPassagesFor(room);

            PrintPassages(type, position, passages, pixels);
        }

        private static void PrintPassages(RoomType type, Vector2Int pixelPos, ChunkPassages passages, Color[][] pixels)
        {
            for (var dx = 1; dx < 4; dx++)
                for (var dy = 1; dy < 4; dy++)
                    pixels[pixelPos.x + dx][pixelPos.y + dy] = GetRoomtypeColor(type);

            if (passages.HasNorth)
                pixels[pixelPos.x + 2][pixelPos.y + 4] = Color.white;
            if (passages.HasEast)
                pixels[pixelPos.x + 4][pixelPos.y + 2] = Color.white;
            if (passages.HasSouth)
                pixels[pixelPos.x + 2][pixelPos.y] = Color.white;
            if (passages.HasWest)
                pixels[pixelPos.x][pixelPos.y + 2] = Color.white;
        }

        private static Color GetRoomtypeColor(RoomType type)
        {
            switch (type)
            {
                case RoomType.Start:
                    return Color.cyan;
                case RoomType.Normal:
                    return Color.white;
                case RoomType.End:
                    return Color.magenta;
            }

            throw new Exception("Invalid room type!");
        }

        private static Vector2Int GetPixelPos(ChunkPosition position, Vector2Int offset)
        {
            var offsetPosition = GetOffsetPosition(position, offset);

            return new Vector2Int(
                offsetPosition.x * 5,
                offsetPosition.y * 5);
        }

        private static Vector2Int GetOffsetPosition(ChunkPosition position, Vector2Int offset)
        {
            return new Vector2Int(
                position.X + offset.x,
                position.Y + offset.y);
        }

        #endregion

    }

}