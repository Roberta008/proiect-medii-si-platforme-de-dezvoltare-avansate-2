using ProiectMPDA.Composite;

namespace ProiectMPDA.Database
{
    public interface IQuery
    {
        IEnumerable<ICompositeItem> Select(params object[] aditionalParameters);

        void Insert(string newNodeValue = "", params object[] aditionalParameters);
        void Delete(params object[] aditionalParameters);

        void Update(string newNodeValue, params object[] aditionalParameters);
    }
}
