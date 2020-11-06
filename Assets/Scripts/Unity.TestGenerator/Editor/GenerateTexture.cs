using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Generation.Editor
{

    internal static class GenerateTexture
    {

        #region Methods

        internal static Texture2D From(FloorPlan floorPlan, TestRoomInfoRespository roomInfoRespository)
        {
            var pixels = ConvertToColorArray(floorPlan, roomInfoRespository);
            var texture = new Texture2D(pixels[0].Length, pixels.Length)
            {
                filterMode = FilterMode.Point
            };

            texture.SetPixels(pixels.SelectMany(x => x).ToArray());
            texture.Apply();

            return texture;
        }

        private static Color[][] ConvertToColorArray(FloorPlan floorPlan, TestRoomInfoRespository roomInfoRespository)
        {
            var pixels = CreateColorArray(floorPlan);
            var offset = CalculateChunkOffset(floorPlan);

            foreach (var room in floorPlan.Rooms)
                PrintRoom(room, offset, roomInfoRespository, pixels);

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
            var minX = floorPlan.Rooms.Select(r => r.Position.X).Min();
            var maxX = floorPlan.Rooms.Select(r => r.Position.X).Max();

            return Mathf.Abs(minX - maxX) + 1;
        }

        private static int GetFloorPlanHeight(FloorPlan floorPlan)
        {
            var minY = floorPlan.Rooms.Select(r => r.Position.Y).Min();
            var maxY = floorPlan.Rooms.Select(r => r.Position.Y).Max();

            return Mathf.Abs(minY - maxY) + 1;
        }

        private static Vector2Int CalculateChunkOffset(FloorPlan floorPlan)
        {
            return new Vector2Int(
                -floorPlan.Rooms.Select(r => r.Position.X).Min(),
                -floorPlan.Rooms.Select(r => r.Position.Y).Min());
        }

        private static void PrintRoom(RoomInChunk room, Vector2Int offset, TestRoomInfoRespository roomInfoRespository, Color[][] pixels)
        {
            var position = GetPixelPos(room.Position, offset);
            var passages = roomInfoRespository.GetById(room.RoomId).Passages;

            PrintPassages(position, passages, pixels);
        }

        private static void PrintPassages(Vector2Int pixelPos, ChunkPassages passages, Color[][] pixels)
        {
            for (var dx = 1; dx < 4; dx++)
                for (var dy = 1; dy < 4; dy++)
                    pixels[pixelPos.x + dx][pixelPos.y + dy] = Color.white;

            if (passages.HasNorth) pixels[pixelPos.x + 2][pixelPos.y + 4] = Color.green;
            if (passages.HasEast) pixels[pixelPos.x + 4][pixelPos.y + 2] = Color.blue;
            if (passages.HasSouth) pixels[pixelPos.x + 2][pixelPos.y] = Color.yellow;
            if (passages.HasWest) pixels[pixelPos.x][pixelPos.y + 2] = Color.red;
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