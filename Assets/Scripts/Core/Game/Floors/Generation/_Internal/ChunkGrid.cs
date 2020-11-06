using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors.Generation
{

    internal class ChunkGrid : IChunkGrid
    {

        #region Fields

        private readonly Dictionary<ChunkPosition, RoomInfo> roomsByChunks = new Dictionary<ChunkPosition, RoomInfo>();

        #endregion

        #region Properties

        public int RoomCount { get { return roomsByChunks.Count; } }

        #endregion

        #region Methods

        public ChunkPosition FindNextBuildChunk(IRNG rng)
        {
            var possiblePositions = GetPossiblePositions();

            if (possiblePositions.Count() > 0)
            {
                return possiblePositions.GetWeightedRandom(GetChunkWeight, rng);
            }
            throw new Exception("Could not find any possible positions!");
        }

        internal float GetChunkWeight(ChunkPosition position)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<ChunkPosition> GetPossiblePositions()
        {
            return roomsByChunks.Keys
                .SelectMany(GenerationUtility.GetSurroundingPositions)
                .Distinct()
                .Where(CanPlaceAt);
        }

        internal bool CanPlaceAt(ChunkPosition position)
        {
            return IsEmpty(position) && HasPassagesTo(position);
        }

        private bool HasPassagesTo(ChunkPosition position)
        {
            return GetPassagesTo(position).Count != 0;
        }


        public void Place(RoomInfo room, ChunkPosition position)
        {
            roomsByChunks.Add(position, room);
        }


        public ChunkPassages GetPassagesTo(ChunkPosition position)
        {
            var hasNorth = HasPassageInto(position, Passage.North);
            var hasEast = HasPassageInto(position, Passage.East);
            var hasSouth = HasPassageInto(position, Passage.South);
            var hasWest = HasPassageInto(position, Passage.West);

            return new ChunkPassages(hasNorth, hasEast, hasSouth, hasWest);
        }

        private bool HasPassageInto(ChunkPosition position, Passage direction)
        {
            var positionInDirection = position + direction;
            var invertedDirection = Invert(direction);

            return HasPassageOutOf(positionInDirection, invertedDirection);
        }

        private Passage Invert(Passage passage)
        {
            switch (passage)
            {
                case Passage.North:
                    return Passage.South;
                case Passage.East:
                    return Passage.West;
                case Passage.South:
                    return Passage.North;
                case Passage.West:
                    return Passage.East;
            }

            throw new Exception("Invalid direction");
        }

        private bool HasPassageOutOf(ChunkPosition position, Passage passage)
        {
            if (IsEmpty(position))
                return false;

            var roomInfo = roomsByChunks[position];
            return roomInfo.Passages.Has(passage);
        }


        public FloorPlan BuildPlan()
        {
            var roomsInChunks = roomsByChunks.Select(kvP => new RoomInChunk(kvP.Value.RoomId, kvP.Key)).ToArray();

            return new FloorPlan(roomsInChunks);
        }


        public bool IsEmpty(ChunkPosition position)
        {
            return !roomsByChunks.ContainsKey(position);
        }


        public ChunkPosition[] FindDeadEndChunks()
        {
            return GetPossiblePositions().Where(IsDeadEnd).ToArray();
        }

        private bool IsDeadEnd(ChunkPosition position)
        {
            return GetPassagesTo(position).Count == 1;
        }

        #endregion

    }

}