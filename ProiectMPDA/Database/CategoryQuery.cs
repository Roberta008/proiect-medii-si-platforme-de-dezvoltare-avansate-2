using ProiectMPDA.Composite;
using ProiectMPDA.Database.Singleton;
using ProiectMPDA.Factory_Method;
using System.Data.SQLite;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8601 // Possible null reference assignment.

namespace ProiectMPDA.Database
{
    public class CategoryQuery : IQuery
    {
        private readonly string SELECT_STORE_CATEGORIES_QUERY =
        """
            SELECT DISTINCT
                  T3.CATEGORY_ID
                , T3.CATEGORY_NAME
            FROM STORES_CATEGORIES_PRODUCTS_LINK AS T1
            JOIN STORES AS T2
                ON T1.STORE_ID = T2.STORE_ID
            JOIN PRODUCT_CATEGORIES AS T3
                ON T1.CATEGORY_ID = T3.CATEGORY_ID
            WHERE T1.STORE_ID = @StoreID; 
        """;

        private readonly string SELECT_CATEGORY_ID =
        """
            SELECT CATEGORY_ID FROM PRODUCT_CATEGORIES
            WHERE CATEGORY_NAME = @CategoryName;
        """;

        private readonly string INSERT_CATEGORY_QUERY =
        """
            INSERT INTO PRODUCT_CATEGORIES (CATEGORY_NAME)
            VALUES (@CategoryName);
        """;

        private readonly string UPDATE_CATEGORY_QUERY =
        """
            UPDATE PRODUCT_CATEGORIES
            SET CATEGORY_NAME = @NewCategoryName
            WHERE CATEGORY_ID = @CategoryID;
        """;

        private readonly SQLiteConnection sQLiteConnection = DatabaseManager.Instance.GetConnection();

        public IEnumerable<ICompositeItem> Select(params object[] aditionalParameters)
        {
            if (aditionalParameters.Length == 0)
            {
                throw new ArgumentException(message: "Specify store ID!");
            }
            List<ICompositeItem> listOfCategories = [];
            int storeID = Convert.ToInt32(value: aditionalParameters[0]);
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = SELECT_STORE_CATEGORIES_QUERY;
            sQLiteCommand.Parameters.AddWithValue(parameterName: "@StoreID", value: storeID);
            using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
            while (sQLiteDataReader.Read())
            {
                int categoryID = sQLiteDataReader.GetInt32(i: 0);
                string categoryName = sQLiteDataReader.GetString(i: 1);
                CategoryArgs categoryArgs = new()
                {
                    ID = categoryID,
                    Name = categoryName,
                    Items = []
                };
                Category newCategory = CompositeItemFactory.Create(itemArgs: categoryArgs);
                listOfCategories.Add(item: newCategory);
            }
            return listOfCategories;
        }

        public void Insert(string newCategoryName, params object[] aditionalParameters)
        {
            if (aditionalParameters.Length == 0)
            {
                throw new ArgumentException(message: "Specify store name!");
            }
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = INSERT_CATEGORY_QUERY;
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "@CategoryName",
                value: newCategoryName
            );
            _ = sQLiteCommand.ExecuteNonQuery();
            TreeNode selectedNode = aditionalParameters[0] as TreeNode;
            LinkTableQuery linkTableQuery = new();

            linkTableQuery.Insert
            (
                aditionalParameters: [TreeNodeType.CATEGORY_NODE, selectedNode, newCategoryName]
            );
        }

        public int SelectCategoryID(string categoryName)
        {
            SQLiteConnection sQLiteConnection = DatabaseManager.Instance.GetConnection();
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = SELECT_CATEGORY_ID;
            sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "CategoryName",
                value: categoryName
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
            int categoryID = SelectCategoryID(categoryName: oldNodeName);
            using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
            sQLiteCommand.CommandText = UPDATE_CATEGORY_QUERY;
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "@NewCategoryName",
                value: newNodeValue
            );
            _ = sQLiteCommand.Parameters.AddWithValue
            (
                parameterName: "@CategoryID",
                value: categoryID
            );
            _ = sQLiteCommand.ExecuteNonQuery();
        }
    }
}

#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
