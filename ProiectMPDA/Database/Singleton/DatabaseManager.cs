using Microsoft.Extensions.Configuration;
using System.Data.SQLite;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace ProiectMPDA.Database.Singleton
{
    public class DatabaseManager
    {
        private static readonly Lazy<DatabaseManager> databaseInstance = new(valueFactory: () => new DatabaseManager());
        private readonly SQLiteConnection databaseConnection;
        public static DatabaseManager Instance => databaseInstance.Value;

        private DatabaseManager()
        {
            var configurationFile =
                new ConfigurationBuilder()
                    .AddJsonFile(path: "appConfiguration.json")
                    .Build();
            string connectionString = configurationFile.GetConnectionString(name: "DefaultConnection");
            string databaseFileName = connectionString.Replace(oldValue: "Data Source=", newValue: string.Empty);
            string databaseFilePath = Path.Combine
            (
                path1: AppDomain.CurrentDomain.BaseDirectory,
                path2: databaseFileName
            );
            if (!File.Exists(path: databaseFilePath))
            {
                SQLiteConnection.CreateFile(databaseFileName: databaseFilePath);

                using SQLiteConnection temporaryConnection = new(connectionString: connectionString);
                temporaryConnection.Open();

                CreateTables(databaseConnection: temporaryConnection);
                AddDataInTables(temporaryConnection: temporaryConnection);
            }
            databaseConnection = new SQLiteConnection(connectionString: connectionString);
            databaseConnection.Open();
        }

        public SQLiteConnection GetConnection()
        {
            return databaseConnection;
        }

        private static void CreateTables(SQLiteConnection databaseConnection)
        {
            string createStoresTableQuery =
            """
                CREATE TABLE IF NOT EXISTS STORES (
                    STORE_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    STORE_NAME TEXT NOT NULL
                );
            """;
            string createProductsCategoryTableQuery =
            """
                CREATE TABLE IF NOT EXISTS PRODUCT_CATEGORIES (
                    CATEGORY_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    CATEGORY_NAME TEXT NOT NULL
                );
            """;
            string createProductsTableQuery =
            """
                CREATE TABLE IF NOT EXISTS PRODUCTS (
                    PRODUCT_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    PRODUCT_NAME TEXT NOT NULL
                );
            """;
            string createStoresProductsCategoriesLinkTable =
            """
                CREATE TABLE IF NOT EXISTS STORES_CATEGORIES_PRODUCTS_LINK (
                    STORE_ID INTEGER,
                    CATEGORY_ID INTEGER,
                    PRODUCT_ID INTEGER,
                    -- PRIMARY KEY (STORE_ID, CATEGORY_ID, PRODUCT_ID),
                    FOREIGN KEY (STORE_ID) REFERENCES STORES(STORE_ID),
                    FOREIGN KEY (CATEGORY_ID) REFERENCES PRODUCT_CATEGORIES(CATEGORY_ID),
                    FOREIGN KEY (PRODUCT_ID) REFERENCES PRODUCTS(PRODUCT_ID)
                );
            """;
            using SQLiteCommand sqlCommand = databaseConnection.CreateCommand();
            sqlCommand.CommandText = createStoresTableQuery;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = createProductsCategoryTableQuery;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = createProductsTableQuery;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = createStoresProductsCategoriesLinkTable;
            sqlCommand.ExecuteNonQuery();
        }
        private static void AddDataInTables(SQLiteConnection temporaryConnection)
        {
            string insertIntoStoresQuery =
            """
                INSERT INTO STORES (STORE_NAME)
                VALUES
                      ('S.C. Teilor S.R.L')             -- STORE_ID = 1
                    , ('S.C. Ludwig von Ybl S.R.L')     -- STORE_ID = 2
                    , ('S.C. Steflesti S.R.L')          -- STORE_ID = 3
            """;
            string insertIntoCategoriesQuery =
            """
                INSERT INTO PRODUCT_CATEGORIES (CATEGORY_NAME)
                VALUES
                      ('Laptop, Tablete & Telefoane')   -- CATEGORY_ID = 1
                    , ('PC, Periferice & Software')     -- CATEGORY_ID = 2
                    , ('TV, Audio-Video & Foto')        -- CATEGORY_ID = 3
            """;
            string insertIntoProductsQuery =
            """
                INSERT INTO PRODUCTS (PRODUCT_NAME)
                VALUES
                      ('Lenovo V15 G4 IRU')             -- PRODUCT_ID = 1
                    , ('Samsung Galaxy A54')            -- PRODUCT_ID = 2
                    , ('Lenovo Tab M8 (4th Gen) 2024')  -- PRODUCT_ID = 3
            """;
            string insertIntoLinkTableQuery =
            """
                INSERT INTO STORES_CATEGORIES_PRODUCTS_LINK (STORE_ID, CATEGORY_ID, PRODUCT_ID)
                VALUES
                      (1, 1, 1)
                    , (1, 1, 2)
                    , (1, 1, 3)
                    , (2, 3, 1)
                    -- , (3, 2, NULL)
            """;
            SQLiteCommand sqlCommand = new(connection: temporaryConnection);
            sqlCommand.CommandText = insertIntoStoresQuery;
            _ = sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = insertIntoCategoriesQuery;
            _ = sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = insertIntoProductsQuery;
            _ = sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = insertIntoLinkTableQuery;
            _ = sqlCommand.ExecuteNonQuery();
        }
    }
}

#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
