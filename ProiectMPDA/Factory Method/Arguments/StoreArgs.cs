using ProiectMPDA.Composite;

namespace ProiectMPDA.Factory_Method
{
    public class StoreArgs : ICompositeItemArgs<Store>
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public List<ICompositeItem> Items { get; set; } = [];
    }
}
