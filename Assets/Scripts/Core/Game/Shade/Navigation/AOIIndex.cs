namespace AChildsCourage.Game.Shade.Navigation
{

    public readonly struct AoiIndex
    {

        public static AoiIndex Zero => 0;


        private readonly int value;


        private AoiIndex(int value) => this.value = value;


        public static implicit operator AoiIndex(int index) => new AoiIndex(index);

        public static implicit operator int(AoiIndex index) => index.value;

    }

}