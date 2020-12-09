namespace AChildsCourage.Game.Shade.Navigation
{

    public readonly struct AoiIndex
    {

        public static AoiIndex Zero => (AoiIndex) 0;


        private readonly int value;


        private AoiIndex(int value) => this.value = value;


        public static explicit operator AoiIndex(int index) => new AoiIndex(index);

        public static implicit operator int(AoiIndex index) => index.value;

    }

}