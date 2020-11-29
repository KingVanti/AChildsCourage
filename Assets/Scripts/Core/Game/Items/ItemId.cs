namespace AChildsCourage.Game.Items
{

    public readonly struct ItemId
    {

        private readonly int value;


        private ItemId(int value)
        {
            this.value = value;
        }


        public override string ToString()
        {
            return value.ToString();
        }


        public static explicit operator ItemId(int id)
        {
            return new ItemId(id);
        }

        public static implicit operator int(ItemId id)
        {
            return id.value;
        }

    }

}