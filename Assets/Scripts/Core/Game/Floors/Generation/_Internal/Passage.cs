namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct Passage
    {

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