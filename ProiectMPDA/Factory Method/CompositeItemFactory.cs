using ProiectMPDA.Composite;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace ProiectMPDA.Factory_Method
{
    public class CompositeItemFactory
    {
        public static T Create<T>(ICompositeItemArgs<T> itemArgs) where T : ICompositeItem
        {
            switch (itemArgs)
            {
                case ICompositeItemArgs<Store>:
                    {
                        StoreArgs storeArgs = itemArgs as StoreArgs;
                        return (T)(object)new Store
                        {
                            StoreID = storeArgs.ID,
                            StoreName = storeArgs.Name,
                            ListOfCategories = storeArgs.Items
                        };
                    }
                case ICompositeItemArgs<Category>:
                    {
                        CategoryArgs categoryArgs = itemArgs as CategoryArgs;
                        return (T)(object)new Category
                        {
                            CategoryID = categoryArgs.ID,
                            CategoryName = categoryArgs.Name,
                            ListOfProducts = categoryArgs.Items
                        };
                    }
                case ICompositeItemArgs<Product>:
                    {
                        ProductArgs productArgs = itemArgs as ProductArgs;
                        return (T)(object)new Product
                        {
                            ProductID = productArgs.ID,
                            ProductName = productArgs.Name,
                            ListOfItems = productArgs.Items
                        };
                    }
                default:
                    throw new ArgumentException(message: "Unsupported itemArgs type.");
            }
        }
    }
}

#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
