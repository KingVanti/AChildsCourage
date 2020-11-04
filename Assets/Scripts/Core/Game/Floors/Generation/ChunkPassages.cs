using System;

namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct ChunkPassages
    {

        #region Properties

        public Passage? North { get; }

        public Passage? East { get; }

        public Passage? South { get; }

        public Passage? West { get; }


        public int Count { get { return (HasNorth ? 1 : 0) + (HasEast ? 1 : 0) + (HasSouth ? 1 : 0) + (HasWest ? 1 : 0); } }


        public bool HasNorth { get { return North.HasValue; } }

        public bool HasEast { get { return East.HasValue; } }

        public bool HasSouth { get { return South.HasValue; } }

        public bool HasWest { get { return West.HasValue; } }

        #endregion

        #region Constructors

        public ChunkPassages(Passage? north, Passage? east, Passage? south, Passage? west)
        {
            North = north;
            East = east;
            South = south;
            West = west;
        }

        #endregion

        #region Methods

        internal Passage? TryGet(PassageDirection direction)
        {
            switch (direction)
            {
                case PassageDirection.North:
                    return North;
                case PassageDirection.East:
                    return East;
                case PassageDirection.South:
                    return South;
                case PassageDirection.West:
                    return West;
            }

            throw new Exception("Invalid direction!");
        }

        #endregion

    }

}