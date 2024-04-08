using ProiectMPDA.Visitor;

namespace ProiectMPDA.Composite
{
    public interface ICompositeItem
    {
        public int GetID();
        public void SetID(int ID);
        public string GetName();
        public void SetName(string itemName);
        public List<ICompositeItem> GetItems();
        public void SetItems(List<ICompositeItem> listOfItems);
        void Accept(ICompositeVisitor compositeVisitor);
    }
}
