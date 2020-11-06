namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct PossibleConnection
    {

        #region Properties

        public Passage First { get; }

        public Passage Second { get; }

        #endregion

        #region Constructors

        public PossibleConnection(Passage first, Passage second)
        {
            First = first;
            Second = second;
        }

        #endregion

    }

}