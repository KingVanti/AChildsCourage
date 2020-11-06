using System;
using System.Collections.Generic;
using System.Linq;

namespace AChildsCourage.Game.Floors.Generation
{

    internal class ChunkGrid : IChunkGrid
    {

        #region Fields

        private readonly Dictionary<ChunkPosition, RoomInfo> roomsByChunks = new Dictionary<ChunkPosition, RoomInfo>();
        private readonly List<ChunkPosition> reservedChunks = new List<ChunkPosition>();

        #endregion

        #region Properties

        public int RoomCount { get { return roomsByChunks.Count; } }

        public int ReservedChunkCount { get { return reservedChunks.Count; } }

        #endregion

        #region Methods

        public ChunkPosition FindNextBuildChunk(IRNG rng)
        {
            if (reservedChunks.Count() > 0)
            {
                return reservedChunks.GetWeightedRandom(GetChunkWeight, rng);
            }
            throw new Exception("Could not find any more possible chunks!");
        }

        internal float GetChunkWeight(ChunkPosition position)
        {
            return 1;
        }


        public void Place(RoomInfo room, ChunkPosition position)
        {
            roomsByChunks.Add(position, room);

            reservedChunks.Remove(position);
            ReserveChunksAround(position);
        }

        private void ReserveChunksAround(ChunkPosition position)
        {
            var reservablePositions = GetReservablePositionsAround(position);

            foreach (var rereservablePosition in reservablePositions)
                reservedChunks.Add(rereservablePosition);
        }

        private IEnumerable<ChunkPosition> GetReservablePositionsAround(ChunkPosition position)
        {
            return
                GenerationUtility.GetSurroundingPositions(position)
                .Where(CanReserve);
        }

        private bool CanReserve(ChunkPosition position)
        {
            return IsUnreserved(position) && CanPlaceAt(position);
        }

        private bool IsUnreserved(ChunkPosition position)
        {
            return !reservedChunks.Contains(position);
        }

        internal bool CanPlaceAt(ChunkPosition position)
        {
            return IsEmpty(position) && IsConnectedToToAnyRoom(position);
        }

        private bool IsEmpty(ChunkPosition position)
        {
            return !roomsByChunks.ContainsKey(position);
        }

        private bool IsConnectedToToAnyRoom(ChunkPosition position)
        {
            var passages = GetPassagesInto(position);

            return passages.Count != 0;
        }

        private ChunkPassages GetPassagesInto(ChunkPosition position)
        {
            var hasNorth = HasPassage(position, Passage.North);
            var hasEast = HasPassage(position, Passage.East);
            var hasSouth = HasPassage(position, Passage.South);
            var hasWest = HasPassage(position, Passage.West);

            return new ChunkPassages(hasNorth, hasEast, hasSouth, hasWest);
        }

        private bool HasPassage(ChunkPosition position, Passage passage)
        {
            var positionInDirection = position + passage;

            if (IsEmpty(positionInDirection))
                return false;

            var roomAtPosition = roomsByChunks[positionInDirection];
            return roomAtPosition.Passages.Has(passage.Invert());
        }


        public ChunkPassageFilter GetFilterFor(ChunkPosition position)
        {
            var north = GetFilterFor(position, Passage.North);
            var east = GetFilterFor(position, Passage.East);
            var south = GetFilterFor(position, Passage.South);
            var west = GetFilterFor(position, Passage.West);

            return new ChunkPassageFilter(north, east, south, west);
        }

        private PassageFilter GetFilterFor(ChunkPosition position, Passage passage)
        {
            var positionInDirection = position + passage;

            if (IsEmpty(positionInDirection))
                return PassageFilter.Either;

            var roomAtPosition = roomsByChunks[positionInDirection];
            var hasPassage = roomAtPosition.Passages.Has(passage.Invert());

            return hasPassage ? PassageFilter.MustHave : PassageFilter.MustNotHave;
        }


        public FloorPlan BuildPlan()
        {
            var roomsInChunks = roomsByChunks.Select(kvP => new RoomInChunk(kvP.Value.RoomId, kvP.Key)).ToArray();

            return new FloorPlan(roomsInChunks);
        }

        #endregion

    }

}