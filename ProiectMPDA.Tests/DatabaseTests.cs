using System.Data.SQLite;
using System.Windows.Forms;
using NUnit.Framework;
using ProiectMPDA.Composite;
using ProiectMPDA.Database;

namespace ProiectMPDA.Tests
{
    public class DatabaseTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DatabaseTests_CreateDatabaseConnection_ReturnSQLiteConnection()
        {
            SQLiteConnection sQLiteConnection = ProiectMPDA.Database.Singleton.DatabaseManager.Instance.GetConnection();
            Assert.That(actual: sQLiteConnection, expression: Is.Not.Null);
        }

        [Test]
        public void DatabaseTests_SelectStores_ReturnIEnumerableICompositeItems()
        {
            StoresQuery storesQuery = new();
            IEnumerable<ICompositeItem> selectedStores = storesQuery.Select();
            Assert.That(selectedStores.Count(), Is.GreaterThan(expected: 0));
        }

        [Test]
        public void DatabaseTests_ModifyStore_ReturnVoid()
        {
            StoresQuery storesQuery = new();
            ICompositeItem firstStore = storesQuery.Select().First();

            string oldStoreName = firstStore.GetName(); // "S.C. Teilor S.R.L"
            string newStoreName = "S.C. Teilor-Test S.R.L";

            Pair<TreeNode, TreeNodeType> nodePair = new
            (
                new TreeNode(text: oldStoreName),
                TreeNodeType.STORE_NODE
            );

            storesQuery.Update(newNodeValue: newStoreName, aditionalParameters: nodePair);

            ICompositeItem updatedStore = storesQuery.Select().First();

            Assert.That(actual: updatedStore.GetName(), expression: Is.EqualTo(expected: newStoreName));
        }

        [Test]
        public void DatabaseTests_DeleteStore_ReturnVoid()
        {
            StoresQuery storesQuery = new();
            ICompositeItem firstStore = storesQuery.Select().First(); // Sequence contains no elements...

            string storeName = firstStore.GetName(); //  S.C. Teilor-Test S.R.L

            Pair<TreeNode, TreeNodeType> nodePair = new
            (
                new TreeNode(text: storeName),
                TreeNodeType.STORE_NODE
            );

            storesQuery.Delete(aditionalParameters: nodePair);

            ICompositeItem firstStoreAfterDelete = storesQuery.Select().First();

            Assert.That(actual: firstStoreAfterDelete.GetName(), expression: Is.Not.EqualTo(expected: firstStore.GetName()));
        }
    }
}