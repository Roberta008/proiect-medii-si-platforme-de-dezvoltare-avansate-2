using ProiectMPDA.Composite;

namespace ProiectMPDA.Visitor
{
    public interface ICompositeVisitor
    {
        void VisitStore(ICompositeItem currentStore);
        void VisitCategory(ICompositeItem currentCategory);
        void VisitProduct(ICompositeItem currentProduct);
    }
}
