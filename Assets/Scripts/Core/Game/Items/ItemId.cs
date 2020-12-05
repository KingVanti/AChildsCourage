namespace AChildsCourage.Game.Items
{

    public readonly struct ItemId
    {

        private readonly int value;


        public ItemId(int value) => this.value = value;


        public override string ToString() => value.ToString();


        public static explicit operator ItemId(int id) => new ItemId(id);

        public static implicit operator int(ItemId id) => id.value;

    }

}