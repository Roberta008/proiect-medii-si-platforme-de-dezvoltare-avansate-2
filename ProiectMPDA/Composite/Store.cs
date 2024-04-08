using ProiectMPDA.Visitor;

namespace ProiectMPDA.Composite
{
    public class Store : ICompositeItem
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public List<ICompositeItem> ListOfCategories { get; set; } = [];

        public int GetID()
        {
            return StoreID;
        }

        public void SetID(int ID)
        {
            StoreID = ID;
        }

        public string GetName()
        {
            return StoreName;
        }

        public void SetName(string itemName)
        {
            StoreName = itemName;
        }

        public List<ICompositeItem> GetItems()
        {
            return ListOfCategories;
        }

        public void SetItems(List<ICompositeItem> listOfItems)
        {
            ListOfCategories = listOfItems;
        }

        public void Accept(ICompositeVisitor compositeVisitor)
        {
            compositeVisitor.VisitStore(currentStore: this);
            foreach (ICompositeItem currentCategory in ListOfCategories)
            {
                currentCategory.Accept(compositeVisitor: compositeVisitor);
            }
        }
    }
}
