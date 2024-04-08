using ProiectMPDA.Command;
using ProiectMPDA.Composite;
using ProiectMPDA.Database;
using ProiectMPDA.Database.Singleton;
using ProiectMPDA.Visitor;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace ProiectMPDA
{
    public partial class MainForm : Form
    {
        private readonly DatabaseManager databaseManagerInstance = DatabaseManager.Instance;
        private List<ICompositeItem> ListOfStores = [];
        private Pair<TreeNode, TreeNodeType> treeNodePair;

        public MainForm() => InitializeComponent();

        private void MainForm_Load(object eventSender, EventArgs eventArgs)
        {
            treeView.Nodes.Add(text: "New Store");
            RetrieveDataFromDatabase();
            AddDataToTreeView();
        }

        private void AddDataToTreeView()
        {
            ICompositeVisitor treeViewVisitor = new TreeViewVisitor { TreeView = treeView };
            foreach (ICompositeItem currentStore in ListOfStores)
            {
                currentStore.Accept(compositeVisitor: treeViewVisitor);
            }
        }

        private void RetrieveDataFromDatabase()
        {
            List<ICompositeItem> listOfStores =
                new StoresQuery().Select().ToList();
            foreach (ICompositeItem currentStore in listOfStores)
            {
                int storeID = currentStore.GetID();
                List<ICompositeItem> listOfCategories =
                    new CategoryQuery().Select(aditionalParameters: storeID).ToList();
                foreach (ICompositeItem currentCategory in listOfCategories)
                {
                    int categoryID = currentCategory.GetID();
                    List<ICompositeItem> listOfProducts =
                        new ProductsQuery().Select(aditionalParameters: [storeID, categoryID]).ToList();
                    currentCategory.SetItems(listOfItems: listOfProducts);
                }
                currentStore.SetItems(listOfItems: listOfCategories);
                ListOfStores.Add(item: currentStore);
            }
        }

        private void TreeView_AfterSelect(object eventSender, TreeViewEventArgs treeViewEventArgs)
        {
            nodeNameTextBox.Enabled = true;
            foreach (var currentButton in flowLayoutPanel.Controls.OfType<Button>())
            {
                currentButton.Enabled = true;
            }
            TreeNode selectedTreeNode = treeView.SelectedNode;
            TreeNodeType typeOfNode = GetTreeNodeType(treeNode: selectedTreeNode);
            treeNodePair = new Pair<TreeNode, TreeNodeType>
            (
                firstItem: selectedTreeNode,
                secondItem: typeOfNode
            );
        }

        private static TreeNodeType GetTreeNodeType(TreeNode treeNode)
        {
            if (treeNode.Parent == null)
            {
                return TreeNodeType.STORE_NODE;
            }
            else if (treeNode.Parent.Parent != null)
            {
                return TreeNodeType.PRODUCT_NODE;
            }
            else
            {
                return TreeNodeType.CATEGORY_NODE;
            }
        }

        private void AddNodeButton_Click(object eventSender, EventArgs eventArgs)
        {
            string userNodeValue = nodeNameTextBox.Text;
            Invoker commandInvoker = new()
            {
                Command = new AddCommand
                (
                    treeView: treeView,
                    treeNodePair: treeNodePair,
                    userNodeValue: userNodeValue
                )
            };
            commandInvoker.ExecuteCommand();
        }

        private void DeleteNodeButton_Click(object eventSender, EventArgs eventArgs)
        {
            Invoker commandInvoker = new()
            {
                Command = new DeleteCommand
                (
                    treeView: treeView,
                    selectedNodePair: treeNodePair
                )
            };
            commandInvoker.ExecuteCommand();
        }

        private void SearchNodeButton_Click(object eventSender, EventArgs eventArgs)
        {
            Invoker commandInvoker = new()
            {
                Command = new SearchCommand
                (
                    treeView: treeView,
                    searchForValue: nodeNameTextBox.Text
                )
            };
            commandInvoker.ExecuteCommand();
        }

        private void ModifyNodeButton_Click(object eventSender, EventArgs eventArgs)
        {
            Invoker commandInvoker = new()
            {
                Command = new ModifyCommand
                (
                    newNodeValue: nodeNameTextBox.Text,
                    treeView: treeView,
                    selectedNodePair: treeNodePair
                )
            };
            commandInvoker.ExecuteCommand();
        }
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
