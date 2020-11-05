namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct PossibleConnection
    {

        #region Properties

        public Passages First { get; }

        public Passages Second { get; }

        #endregion

        #region Constructors

        public PossibleConnection(Passages first, Passages second)
        {
            First = first;
            Second = second;
        }

        #endregion

    }

}