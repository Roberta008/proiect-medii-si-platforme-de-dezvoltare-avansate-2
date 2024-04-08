using ProiectMPDA.Database;

namespace ProiectMPDA.Command
{
    public class ModifyCommand
        (
            string newNodeValue,
            TreeView treeView,
            Pair<TreeNode, TreeNodeType> selectedNodePair
        ) : ICommand
    {
        private string NewNodeValue { get; set; } = newNodeValue;
        private TreeView TreeView { get; set; } = treeView;
        private Pair<TreeNode, TreeNodeType> SelectedNodePair { get; set; } = selectedNodePair;

        public void Execute()
        {
            switch (SelectedNodePair.SecondItem)
            {
                case TreeNodeType.STORE_NODE:
                    StoresQuery storesQuery = new();
                    storesQuery.Update(newNodeValue: NewNodeValue, aditionalParameters: SelectedNodePair);
                    SelectedNodePair.FirstItem.Text = NewNodeValue;
                    break;
                case TreeNodeType.CATEGORY_NODE:
                    CategoryQuery categoryQuery = new();
                    categoryQuery.Update(newNodeValue: NewNodeValue, aditionalParameters: SelectedNodePair);
                    SelectedNodePair.FirstItem.Text = NewNodeValue;
                    break;
                case TreeNodeType.PRODUCT_NODE:
                    ProductsQuery productsQuery = new();
                    productsQuery.Update(newNodeValue: NewNodeValue, aditionalParameters: SelectedNodePair);
                    SelectedNodePair.FirstItem.Text = NewNodeValue;
                    break;
            }
        }
    }
}
