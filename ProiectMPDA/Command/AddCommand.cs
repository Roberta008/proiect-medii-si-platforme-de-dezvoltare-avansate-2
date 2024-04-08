using ProiectMPDA.Database;

namespace ProiectMPDA.Command
{
    public class AddCommand
        (
            TreeView treeView,
            Pair<TreeNode, TreeNodeType> treeNodePair,
            string userNodeValue
        ) : ICommand
    {
        private TreeView TreeView { get; set; } = treeView;
        private Pair<TreeNode, TreeNodeType> Pair { get; set; } = treeNodePair;
        private string UserNodeValue { get; set; } = userNodeValue;

        public void Execute()
        {
            if (string.IsNullOrEmpty(value: UserNodeValue))
            {
                MessageBox.Show
                (
                    text: "Introdu numele noului nod!",
                    caption: "Avertizare",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Exclamation
                );
                return;
            }
            switch (Pair.SecondItem)
            {
                case TreeNodeType.STORE_NODE:
                    if (Pair.FirstItem.Text == "New Store")
                    {
                        _ = TreeView.Nodes.Add(text: UserNodeValue);
                        InsertStore();
                    }
                    else
                    {
                        _ = Pair.FirstItem.Nodes.Add(text: UserNodeValue);
                        InsertCategory();
                    }
                    break;
                case TreeNodeType.CATEGORY_NODE:
                    _ = Pair.FirstItem.Nodes.Add(text: UserNodeValue);
                    InsertProduct(selectedNodeType: TreeNodeType.CATEGORY_NODE);
                    break;
                case TreeNodeType.PRODUCT_NODE:
                    Pair.FirstItem.Parent?.Nodes.Add(text: UserNodeValue);
                    InsertProduct(selectedNodeType: TreeNodeType.PRODUCT_NODE);
                    break;
                default:
                    break;
            }
            Pair.FirstItem.Expand();
        }

        private void InsertStore()
        {
            StoresQuery storesQuery = new();
            storesQuery.Insert(newNodeValue: UserNodeValue);
        }

        private void InsertCategory()
        {
            CategoryQuery categoryQuery = new();
            categoryQuery.Insert(newCategoryName: UserNodeValue, aditionalParameters: Pair.FirstItem);
        }

        private void InsertProduct(TreeNodeType selectedNodeType)
        {
            ProductsQuery productsQuery = new();
            productsQuery.Insert
            (
                newNodeValue: UserNodeValue,
                aditionalParameters: [Pair.FirstItem, selectedNodeType]
            );
        }
    }
}
