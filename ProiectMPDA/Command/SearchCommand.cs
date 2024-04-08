namespace ProiectMPDA.Command
{
    public class SearchCommand(TreeView treeView, string searchForValue) : ICommand
    {
        private TreeView TreeView { get; set; } = treeView;
        private string SearchForValue { get; set; } = searchForValue;
        public void Execute()
        {
            foreach (TreeNode currentStoreNode in TreeView.Nodes)
            {
                if (currentStoreNode.Text.Contains(value: SearchForValue))
                {
                    SetNodeStyle(currentNode: currentStoreNode, nodeType: TreeNodeType.STORE_NODE);
                }
                foreach (TreeNode currentCategoryNode in currentStoreNode.Nodes)
                {
                    if (currentCategoryNode.Text.Contains(value: SearchForValue))
                    {
                        SetNodeStyle(currentNode: currentCategoryNode, nodeType: TreeNodeType.CATEGORY_NODE);
                    }
                    foreach(TreeNode currentProductNode in currentCategoryNode.Nodes)
                    {
                        if (currentProductNode.Text.Contains(value: SearchForValue))
                        {
                            SetNodeStyle(currentNode: currentProductNode, nodeType: TreeNodeType.PRODUCT_NODE);
                        }
                    }
                }
            }
        }

        private static void SetNodeStyle(TreeNode currentNode, TreeNodeType nodeType)
        {
            currentNode.ForeColor = Color.Orange;
            if (nodeType == TreeNodeType.CATEGORY_NODE)
            {
                currentNode.Parent.Expand();
            }
            else if (nodeType == TreeNodeType.PRODUCT_NODE)
            {
                currentNode.Parent.Parent.Expand();
                currentNode.Parent.Expand();
            }
        }
    }
}
