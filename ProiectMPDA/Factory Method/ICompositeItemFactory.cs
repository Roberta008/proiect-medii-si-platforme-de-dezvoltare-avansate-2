using ProiectMPDA.Composite;
using ProiectMPDA.Factory_Method;

namespace ProiectMPDA
{
    public interface ICompositeItemFactory
    {
        T Create<T>(ICompositeItemArgs<T> itemArgs) where T : ICompositeItem;
    }
}
