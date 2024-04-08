using ProiectMPDA.Composite;

namespace ProiectMPDA.Factory_Method
{
    public class ProductArgs : ICompositeItemArgs<Product>
    {
        public int ID { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public List<ICompositeItem> Items { get; set; } = [];
        public int Quantity { get; set; } = 0;
    }
}
