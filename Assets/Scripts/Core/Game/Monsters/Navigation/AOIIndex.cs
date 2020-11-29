namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct AOIIndex
    {

        private readonly int value;


        private AOIIndex(int value)
        {
            this.value = value;
        }


        public static explicit operator AOIIndex(int index)
        {
            return new AOIIndex(index);
        }

        public static implicit operator int(AOIIndex index)
        {
            return index.value;
        }

    }

}