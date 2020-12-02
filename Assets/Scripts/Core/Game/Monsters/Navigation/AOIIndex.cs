namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct AOIIndex
    {

        public static AOIIndex Zero => (AOIIndex) 0;


        private readonly int value;


        private AOIIndex(int value) => this.value = value;


        public static explicit operator AOIIndex(int index) => new AOIIndex(index);

        public static implicit operator int(AOIIndex index) => index.value;

    }

}