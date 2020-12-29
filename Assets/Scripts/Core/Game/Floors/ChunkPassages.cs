using System;

namespace AChildsCourage.Game.Floors
{

    public static class MChunkPassages
    {
        
        public static ChunkPassages AllPassages => new ChunkPassages(true, true, true, true);

        public static Func<ChunkPassages, int> GetPassageCount =>
            passages => (passages.HasNorth ? 1 : 0) +
                        (passages.HasEast ? 1 : 0) +
                        (passages.HasSouth ? 1 : 0) +
                        (passages.HasWest ? 1 : 0);

        public static Func<ChunkPassages, ChunkPassages> Rotate =>
            passages =>
                new ChunkPassages(passages.HasWest, passages.HasNorth, passages.HasEast, passages.HasSouth);

        public static Func<ChunkPassages, ChunkPassages> MirrorOverXAxis =>
            passages =>
                new ChunkPassages(passages.HasSouth, passages.HasEast, passages.HasNorth, passages.HasWest);

        public static Func<ChunkPassages, PassageDirection, bool> HasPassageWithDirection =>
            (passages, direction) =>
            {
                switch (direction)
                {
                    case PassageDirection.North: return passages.HasNorth;
                    case PassageDirection.East: return passages.HasEast;
                    case PassageDirection.South: return passages.HasSouth;
                    case PassageDirection.West: return passages.HasWest;
                    default: throw new Exception("Invalid passage!");
                }
            };


        public readonly struct ChunkPassages
        {

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


            public override string ToString() => $"({(HasNorth ? "North, " : "")}{(HasEast ? "East, " : "")}{(HasSouth ? "South, " : "")}{(HasWest ? "West" : "")})";

        }

    }

}