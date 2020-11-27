namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct AOI
    {

        private AOIIndex Index { get; }


        private AOI(AOIIndex index)
        {
            Index = index;
        }


        public override string ToString()
        {
            return $"AOI {{ Id: {Index} }}";
        }

    }

}