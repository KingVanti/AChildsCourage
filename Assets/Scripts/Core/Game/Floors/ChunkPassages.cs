using System;

namespace AChildsCourage.Game.Floors
{

    public readonly struct ChunkPassages
    {

        internal static int GetPassageCount(ChunkPassages passages) =>
            (passages.HasNorth ? 1 : 0) +
            (passages.HasEast ? 1 : 0) +
            (passages.HasSouth ? 1 : 0) +
            (passages.HasWest ? 1 : 0);

        internal static ChunkPassages Rotate(ChunkPassages passages) =>
            new ChunkPassages(passages.HasWest, passages.HasNorth, passages.HasEast, passages.HasSouth);

        internal static ChunkPassages MirrorOverXAxis(ChunkPassages passages) =>
            new ChunkPassages(passages.HasSouth, passages.HasEast, passages.HasNorth, passages.HasWest);

        internal static bool HasPassageWithDirection(PassageDirection direction, ChunkPassages passages)
        {
            switch (direction)
            {
                case PassageDirection.North: return passages.HasNorth;
                case PassageDirection.East: return passages.HasEast;
                case PassageDirection.South: return passages.HasSouth;
                case PassageDirection.West: return passages.HasWest;
                default: throw new Exception("Invalid passage!");
            }
        }


        public bool HasNorth { get; }

        public bool HasEast { get; }

        public bool HasSouth { get; }

        public bool HasWest { get; }


        public ChunkPassages(bool hasNorth, bool hasEast, bool hasSouth, bool hasWest)
        {
            HasNorth = hasNorth;
            HasEast = hasEast;
            HasSouth = hasSouth;
            HasWest = hasWest;
        }


        public override string ToString() =>
            $"({(HasNorth ? "North, " : "")}{(HasEast ? "East, " : "")}{(HasSouth ? "South, " : "")}{(HasWest ? "West" : "")})";

    }

}