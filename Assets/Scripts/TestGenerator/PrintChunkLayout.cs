using System.Linq;
using UnityEngine;
using static AChildsCourage.Game.Floors.Gen.ChunkLayout;
using static AChildsCourage.Game.Chunk;

namespace AChildsCourage.Game.Floors.Gen
{

    internal static class PrintChunkLayout
    {

        private static readonly Color backgroundColor = Color.clear;
        private static readonly Color chunkCenterColor = new Color(0.21f, 0.51f, 0.11f);
        private static readonly Color startChunkCenterColor = new Color(0.49f, 0.51f, 0.16f);
        private static readonly Color chunkEdgeColor = new Color(0.26f, 0.66f, 0.13f);


        internal static Texture2D PrintToTexture(this ChunkLayout layout)
        {
            var texture = layout
                          .Map(GetDimensions)
                          .Map(TextureUtility.CreateFittingTexture);
            texture.FillWith(backgroundColor);
            PrintFloorLayoutTo(layout, texture);
            return texture;
        }

        private static void PrintFloorLayoutTo(ChunkLayout layout, Texture2D texture)
        {
            var print = CreatePrinter(layout, texture);

            bool CoordIsOnEdge(int coord) =>
                coord % ChunkSize == 0 || (coord + 1) % ChunkSize == 0;

            bool PositionIsOnEdge(TilePosition position) =>
                CoordIsOnEdge(position.X) || CoordIsOnEdge(position.Y);

            bool CoordIsInStartChunk(int coord) =>
                coord >= 0 && coord < ChunkSize;

            bool PositionIsInStartChunk(TilePosition position) =>
                CoordIsInStartChunk(position.X) && CoordIsInStartChunk(position.Y);

            Color GetChunkColor(TilePosition position) =>
                PositionIsOnEdge(position) ? chunkEdgeColor :
                PositionIsInStartChunk(position) ? startChunkCenterColor
                : chunkCenterColor;

            foreach (var position in layout.Map(GetPositions).SelectMany(GetPositionsInChunk)) print(position, GetChunkColor(position));

            texture.Apply();
        }

        private static SetTexturePixel CreatePrinter(ChunkLayout layout, Texture2D texture)
        {
            var minPos = layout.Map(GetPositions).Map(GetLowerLeft).Map(Absolute).Map(GetCorner);

            return (pos, col) => texture.SetPixel(pos.X + minPos.X, pos.Y + minPos.Y, col);
        }


        private delegate void SetTexturePixel(TilePosition position, Color color);

    }

}