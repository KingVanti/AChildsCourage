namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct PossibleConnection
    {
        
        #region Properties

        public PassageDirection First { get; }

        public PassageDirection Second { get; }

        #endregion

        #region Constructors

        public PossibleConnection(PassageDirection first, PassageDirection second)
        {
            First = first;
            Second = second;
        }

        #endregion

    }

}