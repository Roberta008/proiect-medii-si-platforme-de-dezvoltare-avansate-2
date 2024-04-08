using ProiectMPDA.Composite;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace ProiectMPDA.Visitor
{
    public class TreeViewVisitor : ICompositeVisitor
    {
        public TreeView TreeView { get; set; }

        private TreeNode temporaryStoreNode, temporaryCategoryNode;

        public void VisitStore(ICompositeItem currentStore)
        {
            TreeNode storeNode = new() { Text = currentStore.GetName() };
            temporaryStoreNode = storeNode;
            TreeView.Nodes.Add(node: storeNode);
        }

        public void VisitCategory(ICompositeItem currentCategory)
        {
            TreeNode categoryNode = new() { Text = currentCategory.GetName() };
            temporaryCategoryNode = categoryNode;
            temporaryStoreNode.Nodes.Add(node: categoryNode);
        }

        public void VisitProduct(ICompositeItem currentProduct)
        {
            TreeNode productNode = new() { Text = currentProduct.GetName() };
            temporaryCategoryNode.Nodes.Add(node: productNode);
        }
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
