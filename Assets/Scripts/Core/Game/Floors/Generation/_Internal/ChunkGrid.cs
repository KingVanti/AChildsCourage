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
            var northPassage = TryGetPassageInto(position, PassageDirection.North);
            var eastPassage = TryGetPassageInto(position, PassageDirection.East);
            var southPassage = TryGetPassageInto(position, PassageDirection.South);
            var westPassage = TryGetPassageInto(position, PassageDirection.West);

            return new ChunkPassages(northPassage, eastPassage, southPassage, westPassage);
        }

        private Passage? TryGetPassageInto(ChunkPosition position, PassageDirection direction)
        {
            var positionInDirection = position + direction;
            var invertedDirection = Invert(direction);

            return TryGetPassageOutOf(positionInDirection, invertedDirection);
        }

        private PassageDirection Invert(PassageDirection direction)
        {
            switch (direction)
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

        private Passage? TryGetPassageOutOf(ChunkPosition position, PassageDirection direction)
        {
            if (IsEmpty(position))
                return null;

            var roomInfo = roomsByChunks[position];
            return roomInfo.Passages.TryGet(direction);
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