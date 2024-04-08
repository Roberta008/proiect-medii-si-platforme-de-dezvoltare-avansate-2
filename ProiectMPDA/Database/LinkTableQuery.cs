using ProiectMPDA.Composite;
using ProiectMPDA.Database.Singleton;
using System.Data.SQLite;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.

namespace ProiectMPDA.Database
{
    public class LinkTableQuery : IQuery
    {
        private readonly string INSERT_CATEGORY_INTO_LINKER_TABLE_QUERY =
        """
            INSERT INTO STORES_CATEGORIES_PRODUCTS_LINK (STORE_ID, CATEGORY_ID)
            VALUES (@StoreID, @CategoryID);
        """;

        private readonly string INSERT_PRODUCT_INTO_LINKER_TABLE_QUERY =
        """
            INSERT INTO STORES_CATEGORIES_PRODUCTS_LINK (STORE_ID, CATEGORY_ID, PRODUCT_ID)
            VALUES (@StoreID, @CategoryID, @ProductID);
        """;

        private readonly string DELETE_STORE_QUERY =
        """
            DELETE FROM STORES_CATEGORIES_PRODUCTS_LINK
            WHERE STORE_ID = @StoreID;
        """;

        private readonly string DELETE_CATEGORY_QUERY =
        """
            DELETE FROM STORES_CATEGORIES_PRODUCTS_LINK
            WHERE STORE_ID = @StoreID AND CATEGORY_ID = @CategoryID;
        """;

        private readonly string DELETE_PRODUCT_QUERY =
        """
            DELETE FROM STORES_CATEGORIES_PRODUCTS_LINK
            WHERE STORE_ID = @StoreID AND CATEGORY_ID = @CategoryID AND PRODUCT_ID = @ProductID;
        """;

        private readonly SQLiteConnection sQLiteConnection = DatabaseManager.Instance.GetConnection();

        public IEnumerable<ICompositeItem> Select(params object[] aditionalParameters)
        {
            throw new NotImplementedException();
        }

        public void Insert(string newNodeValue = "", params object[] aditionalParameters)
        {
            if (aditionalParameters.Length == 0)
            {
                throw new ArgumentException(message: "Specify all arguments!");
            }
            TreeNodeType newNodeType = (TreeNodeType)aditionalParameters[0];
            switch (newNodeType)
            {
                case TreeNodeType.CATEGORY_NODE:
                    InsertCategory(aditionalParameters);
                    break;

                case TreeNodeType.PRODUCT_NODE:
                    InsertProduct(aditionalParameters);
                    break;
                default:
                    throw new ArgumentException(message: "Invalid node type!");
            }
        }

        public void Delete(params object[] aditionalParameters)
        {
            if (aditionalParameters.Length == 0)
            {
                throw new ArgumentException(message: "Specify all arguments!");
            }

            Pair<TreeNode, TreeNodeType> selectedNodePair = (Pair<TreeNode, TreeNodeType>)aditionalParameters[0];

            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();

            string storeName, categoryName, productName;
            int storeID, productID, categoryID;

            StoresQuery storeQuery = new();
            CategoryQuery categoryQuery = new();
            ProductsQuery productsQuery = new();

            switch (selectedNodePair.SecondItem)
            {
                case TreeNodeType.STORE_NODE:
                    storeName = selectedNodePair.FirstItem.Text;
                    storeID = storeQuery.SelectStoreID(storeName: storeName);
                    sQLiteCommand.CommandText = DELETE_STORE_QUERY;
                    _ = sQLiteCommand.Parameters.AddWithValue(parameterName: "@StoreID", value: storeID);
                    break;
                case TreeNodeType.CATEGORY_NODE:
                    storeName = selectedNodePair.FirstItem.Parent.Text;
                    categoryName = selectedNodePair.FirstItem.Text;
                    storeID = storeQuery.SelectStoreID(storeName: storeName);
                    categoryID = categoryQuery.SelectCategoryID(categoryName: categoryName);
                    sQLiteCommand.CommandText = DELETE_CATEGORY_QUERY;
                    _ = sQLiteCommand.Parameters.AddWithValue(parameterName: "@StoreID", value: storeID);
                    _ = sQLiteCommand.Parameters.AddWithValue(parameterName: "@CategoryID", value: categoryID);
                    break;
                case TreeNodeType.PRODUCT_NODE:
                    storeName = selectedNodePair.FirstItem.Parent.Parent.Text;
                    categoryName = selectedNodePair.FirstItem.Parent.Text;
                    productName = selectedNodePair.FirstItem.Text;
                    storeID = storeQuery.SelectStoreID(storeName: storeName);
                    categoryID = categoryQuery.SelectCategoryID(categoryName: categoryName);
                    productID = productsQuery.SelectProductID(productName: productName);
                    sQLiteCommand.CommandText = DELETE_PRODUCT_QUERY;
                    _ = sQLiteCommand.Parameters.AddWithValue(parameterName: "@StoreID", value: storeID);
                    _ = sQLiteCommand.Parameters.AddWithValue(parameterName: "@CategoryID", value: categoryID);
                    _ = sQLiteCommand.Parameters.AddWithValue(parameterName: "@ProductID", value: productID);
                    break;
            }
            _ = sQLiteCommand.ExecuteNonQuery();
        }

        private void InsertCategory(object[] aditionalParameters)
        {
            TreeNode selectedNode = (TreeNode)aditionalParameters[1];
            string storeName = selectedNode.Text, categoryName = aditionalParameters[2].ToString();
            StoresQuery storesQuery = new();
            CategoryQuery categoryQuery = new();
            int storeID = storesQuery.SelectStoreID(storeName: storeName);
            int categoryID = categoryQuery.SelectCategoryID(categoryName: categoryName);
            SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = INSERT_CATEGORY_INTO_LINKER_TABLE_QUERY;
            _ = sQLiteCommand.Parameters.AddWithValue
            (
            parameterName: "StoreID",
                value: storeID
            );
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "CategoryID",
                value: categoryID
            );
            _ = sQLiteCommand.ExecuteNonQuery();
        }

        private void InsertProduct(object[] aditionalParameters)
        {
            TreeNode selectedNode = (TreeNode)aditionalParameters[1];
            TreeNodeType selectedNodeType = (TreeNodeType)aditionalParameters[2];
            string storeName, categoryName, productName = aditionalParameters[3].ToString();
            switch (selectedNodeType)
            {
                case TreeNodeType.CATEGORY_NODE:
                    storeName = selectedNode.Parent.Text;
                    categoryName = selectedNode.Text;
                    break;
                default:
                    storeName = selectedNode.Parent.Parent.Text;
                    categoryName = selectedNode.Parent.Text;
                    break;
            }
            StoresQuery storesQuery = new();
            CategoryQuery categoryQuery = new();
            ProductsQuery productsQuery = new();
            int storeID = storesQuery.SelectStoreID(storeName: storeName);
            int categoryID = categoryQuery.SelectCategoryID(categoryName: categoryName);
            int productID = productsQuery.SelectProductID(productName: productName);
            SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = INSERT_PRODUCT_INTO_LINKER_TABLE_QUERY;
            _ = sQLiteCommand.Parameters.AddWithValue
            (
            parameterName: "StoreID",
                value: storeID
            );
            _ = sQLiteCommand.Parameters.AddWithValue
            (
            parameterName: "CategoryID",
                value: categoryID
            );
            _ = sQLiteCommand.Parameters.AddWithValue
            (
            parameterName: "ProductID",
                value: productID
            );
            _ = sQLiteCommand.ExecuteNonQuery();
        }

        public void Update(string newNodeValue, params object[] aditionalParameters)
        {
            throw new NotImplementedException();
        }
    }
}

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
