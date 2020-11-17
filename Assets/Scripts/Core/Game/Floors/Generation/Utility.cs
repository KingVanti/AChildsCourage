using System;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {
  
        internal static ChunkPosition MoveToAdjacentChunk(this ChunkPosition position, PassageDirection direction)
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


        internal static PassageDirection Invert(this PassageDirection passage)
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


        internal static GenerationPhase GetCurrentPhase(this int currentRoomCount)
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

    }

}