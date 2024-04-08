using ProiectMPDA.Visitor;

namespace ProiectMPDA.Composite
{
    public class Product : ICompositeItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public List<ICompositeItem> ListOfItems { get; set; } = [];

        public int GetID()
        {
            return ProductID;
        }

        public void SetID(int ID)
        {
            ProductID = ID;
        }

        public string GetName()
        {
            return ProductName;
        }

        public void SetName(string itemName)
        {
            ProductName = itemName;
        }

        public List<ICompositeItem> GetItems()
        {
            return ListOfItems;
        }

        public void SetItems(List<ICompositeItem> listOfItems)
        {
            ListOfItems = (List<ICompositeItem>)listOfItems;
        }

        public void Accept(ICompositeVisitor compositeVisitor)
        {
            compositeVisitor.VisitProduct(currentProduct: this);
        }
    }
}
