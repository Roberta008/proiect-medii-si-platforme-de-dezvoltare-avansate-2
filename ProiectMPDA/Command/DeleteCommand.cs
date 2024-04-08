
using ProiectMPDA.Database;

namespace ProiectMPDA.Command
{
    public class DeleteCommand(TreeView treeView, Pair<TreeNode, TreeNodeType> selectedNodePair) : ICommand
    {
        private TreeView TreeView { get; set; } = treeView;
        private Pair<TreeNode, TreeNodeType> SelectedNodePair { get; set; } = selectedNodePair;

        public void Execute()
        {
            LinkTableQuery linkTableQuery = new();
            StoresQuery storesQuery = new();
            linkTableQuery.Delete(aditionalParameters: SelectedNodePair);
            storesQuery.Delete(aditionalParameters: SelectedNodePair);
            TreeView.Nodes.Remove(node: SelectedNodePair.FirstItem);
        }
    }
}
