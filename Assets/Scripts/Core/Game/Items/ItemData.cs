namespace AChildsCourage.Game.Items
{

    public class ItemData
    {

        public ItemId Id { get; }

        public string Name { get; }


        public ItemData(ItemId id, string name)
        {
            Id = id;
            Name = name;
        }

    }

}