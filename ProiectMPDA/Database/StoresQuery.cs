using ProiectMPDA.Composite;
using ProiectMPDA.Database.Singleton;
using ProiectMPDA.Factory_Method;
using System.Data.SQLite;

namespace ProiectMPDA.Database
{
    public class StoresQuery : IQuery
    {
        private readonly string SELECT_STORE_QUERY =
        """
            SELECT
                  STORE_ID
                , STORE_NAME
            FROM STORES;
        """;

        private readonly string SELECT_STORE_ID_QUERY =
        """
            SELECT STORE_ID FROM STORES
            WHERE STORE_NAME = @StoreName;
        """;

        private readonly string INSERT_STORE_QUERY =
         """
            INSERT INTO STORES (STORE_NAME)
            VALUES (@StoreName);
         """;

        private readonly string DELETE_STORE_QUERY =
            """
                DELETE FROM STORES
                WHERE STORE_ID = @StoreID;
            """;

        private readonly string UPDATE_STORE_QUERY =
        """
            UPDATE STORES
            SET STORE_NAME = @NewStoreName
            WHERE STORE_ID = @StoreID;
        """;

        private readonly SQLiteConnection sQLiteConnection = DatabaseManager.Instance.GetConnection();

        public IEnumerable<ICompositeItem> Select(params object[] aditionalFilterValues)
        {
            List<ICompositeItem> listOfStores = [];
            SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = SELECT_STORE_QUERY;
            using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
            while (sQLiteDataReader.Read())
            {
                int storeID = sQLiteDataReader.GetInt32(i: 0);
                string storeName = sQLiteDataReader.GetString(i: 1);
                StoreArgs storeArgs = new()
                {
                    ID = storeID,
                    Name = storeName,
                    Items = []
                };
                Store newStore = CompositeItemFactory.Create(itemArgs: storeArgs);
                listOfStores.Add(item: newStore);
            }
            return listOfStores;
        }

        public void Insert(string newNodeValue, params object[] aditionalFilterValues)
        {
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = INSERT_STORE_QUERY;
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "@StoreName",
                value: newNodeValue
            );
            _ = sQLiteCommand.ExecuteNonQuery();
        }

        public void Delete(params object[] aditionalParameters)
        {
            if (aditionalParameters.Length == 0)
            {
                throw new ArgumentException(message: "Specify all arguments!");
            }
            Pair<TreeNode, TreeNodeType> selectedNode = (Pair<TreeNode, TreeNodeType>)aditionalParameters[0];
            string storeName = selectedNode.FirstItem.Text;
            int storeID = SelectStoreID(storeName: storeName);
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = DELETE_STORE_QUERY;
            sQLiteCommand.Parameters.AddWithValue("@StoreID", storeID);
            sQLiteCommand.ExecuteNonQuery();
        }

        public void Update(string newNodeValue, params object[] aditionalParameters)
        {
            if (aditionalParameters.Length == 0)
            {
                throw new ArgumentException(message: "Specify all arguments!");
            }
            Pair<TreeNode, TreeNodeType> selectedNodePair = (Pair<TreeNode, TreeNodeType>)aditionalParameters[0];
            string oldNodeName = selectedNodePair.FirstItem.Text;
            int storeID = SelectStoreID(storeName: oldNodeName);
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = UPDATE_STORE_QUERY;
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "@NewStoreName",
                value: newNodeValue
            );
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "@StoreID",
                value: storeID
            );
            _ = sQLiteCommand.ExecuteNonQuery();
        }

        public int SelectStoreID(string storeName)
        {
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = SELECT_STORE_ID_QUERY;
            sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "StoreName",
                value: storeName
            );
            return Convert.ToInt32(value: sQLiteCommand.ExecuteScalar());
        }
    }
}
