namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct Passage
    {

        #region Static Properties

        public static Passage NorthFirst { get { return new Passage(PassageDirection.North, PassageIndex.First); } }

        public static Passage NorthSecond { get { return new Passage(PassageDirection.North, PassageIndex.Second); } }

        public static Passage NorthThird { get { return new Passage(PassageDirection.North, PassageIndex.Third); } }

        public static Passage EastFirst { get { return new Passage(PassageDirection.East, PassageIndex.First); } }

        public static Passage EastSecond { get { return new Passage(PassageDirection.East, PassageIndex.Second); } }

        public static Passage EastThird { get { return new Passage(PassageDirection.East, PassageIndex.Third); } }

        public static Passage SouthFirst { get { return new Passage(PassageDirection.South, PassageIndex.First); } }

        public static Passage SouthSecond { get { return new Passage(PassageDirection.South, PassageIndex.Second); } }

        public static Passage SouthThird { get { return new Passage(PassageDirection.South, PassageIndex.Third); } }

        public static Passage WestFirst { get { return new Passage(PassageDirection.West, PassageIndex.First); } }

        public static Passage WestSecond { get { return new Passage(PassageDirection.West, PassageIndex.Second); } }

        public static Passage WestThird { get { return new Passage(PassageDirection.West, PassageIndex.Third); } }

        #endregion

        #region Properties

        public PassageDirection Direction { get; }

        public PassageIndex Index { get; }

        #endregion

        #region Constructors

        public Passage(PassageDirection direction, PassageIndex index)
        {
            Direction = direction;
            Index = index;
        }

        #endregion

    }

}