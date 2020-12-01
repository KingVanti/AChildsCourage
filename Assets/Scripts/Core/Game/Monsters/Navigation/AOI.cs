namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct AOI
    {

        internal AOIIndex Index { get; }
        
        internal  TilePosition Center { get; }


        internal AOI(AOIIndex index, TilePosition center)
        {
            Index = index;
            Center = center;
        }


        public override string ToString()
        {
            return $"AOI {{ Id: {Index} }}";
        }

    }

}