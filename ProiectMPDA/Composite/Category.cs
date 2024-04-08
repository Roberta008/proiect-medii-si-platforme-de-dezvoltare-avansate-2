using ProiectMPDA.Visitor;

namespace ProiectMPDA.Composite
{
    public class Category : ICompositeItem
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<ICompositeItem> ListOfProducts { get; set; } = [];

        public string GetName()
        {
            return CategoryName;
        }

        public void SetName(string itemName)
        {
            CategoryName = itemName;
        }

        public int GetID()
        {
            return CategoryID;
        }

        public void SetID(int ID)
        {
            CategoryID = ID;
        }

        public List<ICompositeItem> GetItems()
        {
            return ListOfProducts;
        }

        public void SetItems(List<ICompositeItem> listOfItems)
        {
            ListOfProducts = listOfItems;
        }

        public void Accept(ICompositeVisitor compositeVisitor)
        {
            compositeVisitor.VisitCategory(currentCategory: this);
            foreach (ICompositeItem currentProduct in ListOfProducts)
            {
                currentProduct.Accept(compositeVisitor: compositeVisitor);
            }
        }
    }
}
