using AChildsCourage.Game.Floors;
using System;

using static AChildsCourage.Game.NightManagement.Loading.FloorPlanGenerating;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class FloorGenerationUtility
    {

        internal static ChunkPosition MoveToAdjacentChunk(ChunkPosition position, PassageDirection direction)
        {
            switch (direction)
            {
                case PassageDirection.North:
                    return new ChunkPosition(position.X, position.Y + 1);
                case PassageDirection.East:
                    return new ChunkPosition(position.X + 1, position.Y);
                case PassageDirection.South:
                    return new ChunkPosition(position.X, position.Y - 1);
                case PassageDirection.West:
                    return new ChunkPosition(position.X - 1, position.Y);
            }

            throw new Exception("Invalid direction!");
        }


        internal static PassageDirection Invert(PassageDirection passage)
        {
            switch (passage)
            {
                case PassageDirection.North:
                    return PassageDirection.South;
                case PassageDirection.East:
                    return PassageDirection.West;
                case PassageDirection.South:
                    return PassageDirection.North;
                case PassageDirection.West:
                    return PassageDirection.East;
            }

            throw new Exception("Invalid direction");
        }


        internal static GenerationPhase GetCurrentPhase(int currentRoomCount)
        {
            switch (currentRoomCount)
            {
                case 0:
                    return GenerationPhase.StartRoom;
                case GoalRoomCount - 1:
                    return GenerationPhase.EndRoom;
                default:
                    return GenerationPhase.NormalRooms;
            }
        }


        internal static bool IsEmpty(FloorPlanBuilder builder, ChunkPosition position)
        {
            return !builder.RoomsByChunks.ContainsKey(position);
        }


        internal static int CountRooms(FloorPlanBuilder builder)
        {
            return builder.RoomsByChunks.Count;
        }

    }

}