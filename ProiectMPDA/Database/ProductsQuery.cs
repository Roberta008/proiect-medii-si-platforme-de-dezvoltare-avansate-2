using ProiectMPDA.Composite;
using ProiectMPDA.Database.Singleton;
using ProiectMPDA.Factory_Method;
using System.Data.SQLite;

namespace ProiectMPDA.Database
{
    public class ProductsQuery : IQuery
    {
        private readonly string SELECT_PRODUCTS_QUERY =
        """
            SELECT
                  T3.PRODUCT_ID
                , T3.PRODUCT_NAME
            FROM STORES_CATEGORIES_PRODUCTS_LINK AS T1
            JOIN PRODUCT_CATEGORIES AS T2
                ON T1.CATEGORY_ID = T2.CATEGORY_ID
            JOIN PRODUCTS AS T3
                ON T1.PRODUCT_ID = T3.PRODUCT_ID
            WHERE T1.STORE_ID = @StoreID AND T1.CATEGORY_ID = @CategoryID;
        """;

        private readonly string SELECT_PRODUCT_ID_QUERY =
        """
            SELECT PRODUCT_ID FROM PRODUCTS
            WHERE PRODUCT_NAME = @ProductName;
        """;

        private readonly string INSERT_PRODUCT_QUERY =
        """
            INSERT INTO PRODUCTS (PRODUCT_NAME)
            VALUES (@ProductName);
        """;

        private readonly string UPDATE_PRODUCT_QUERY =
        """
            UPDATE PRODUCTS
            SET PRODUCT_NAME = @NewProductName
            WHERE PRODUCT_ID = @ProductID;
        """;

        private readonly SQLiteConnection sQLiteConnection = DatabaseManager.Instance.GetConnection();

        public IEnumerable<ICompositeItem> Select(params object[] aditionalParameters)
        {
            if (aditionalParameters.Length == 0)
            {
                throw new ArgumentException(message: "Specify store ID!");
            }
            List<ICompositeItem> listOfProducts = [];
            int storeID = Convert.ToInt32(value: aditionalParameters[0]);
            int categoryID = Convert.ToInt32(value: aditionalParameters[1]);
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = SELECT_PRODUCTS_QUERY;
            sQLiteCommand.Parameters.AddWithValue(parameterName: "@StoreID", value: storeID);
            sQLiteCommand.Parameters.AddWithValue(parameterName: "@CategoryID", value: categoryID);
            using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
            while (sQLiteDataReader.Read())
            {
                int productID = sQLiteDataReader.GetInt32(i: 0);
                string productName = sQLiteDataReader.GetString(i: 1);
                ProductArgs productArgs = new()
                {
                    ID = productID,
                    Name = productName,
                    Items = []
                };
                Product newProduct = CompositeItemFactory.Create(itemArgs: productArgs);
                listOfProducts.Add(item: newProduct);
            }
            return listOfProducts;
        }

        public void Insert(string newNodeValue, params object[] aditionalParameters)
        {
            if (aditionalParameters.Length == 0)
            {
                throw new ArgumentException(message: "Specify all arguments!");
            }

            TreeNode selectedNode = (TreeNode)aditionalParameters[0];
            /* it can be TreeNodeType.CATEGORY or TreeNodeType.PRODUCT */
            TreeNodeType selectedNodeType = (TreeNodeType)aditionalParameters[1];

            string newProductName = newNodeValue;

            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = INSERT_PRODUCT_QUERY;
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "@ProductName",
                value: newProductName
            );
            _ = sQLiteCommand.ExecuteNonQuery();

            LinkTableQuery linkTableQuery = new();
            linkTableQuery.Insert
            (
                aditionalParameters: [TreeNodeType.PRODUCT_NODE, selectedNode, selectedNodeType, newProductName]
            );
        }

        public int SelectProductID(string productName)
        {
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = SELECT_PRODUCT_ID_QUERY;
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "@ProductName",
                value: productName
            );
            return Convert.ToInt32(value: sQLiteCommand.ExecuteScalar());
        }

        public void Delete(params object[] aditionalParameters)
        {
            throw new NotImplementedException();
        }

        public void Update(string newNodeValue, params object[] aditionalParameters)
        {
            if (aditionalParameters.Length == 0)
            {
                throw new ArgumentException(message: "Specify all arguments!");
            }
            Pair<TreeNode, TreeNodeType> selectedNodePair = (Pair<TreeNode, TreeNodeType>)aditionalParameters[0];
            string oldNodeName = selectedNodePair.FirstItem.Text;
            int productID = SelectProductID(productName: oldNodeName);
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = UPDATE_PRODUCT_QUERY;
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "@NewProductName",
                value: newNodeValue
            );
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "@ProductID",
                value: productID
            );
            _ = sQLiteCommand.ExecuteNonQuery();
        }
    }
}
