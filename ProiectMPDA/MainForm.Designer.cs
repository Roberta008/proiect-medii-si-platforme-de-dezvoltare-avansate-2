
namespace ProiectMPDA
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            addNodeButton = new Button();
            deleteNodeButton = new Button();
            label1 = new Label();
            treeView = new TreeView();
            flowLayoutPanel = new FlowLayoutPanel();
            searchNodeButton = new Button();
            modifyNodeButton = new Button();
            controlsPanel = new Panel();
            nodeNameTextBox = new TextBox();
            label2 = new Label();
            button1 = new Button();
            flowLayoutPanel.SuspendLayout();
            controlsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // addNodeButton
            // 
            addNodeButton.Enabled = false;
            addNodeButton.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            addNodeButton.Location = new Point(4, 4);
            addNodeButton.Margin = new Padding(4);
            addNodeButton.Name = "addNodeButton";
            addNodeButton.Size = new Size(275, 52);
            addNodeButton.TabIndex = 1;
            addNodeButton.Text = "Adauga";
            addNodeButton.UseVisualStyleBackColor = true;
            addNodeButton.Click += AddNodeButton_Click;
            // 
            // deleteNodeButton
            // 
            deleteNodeButton.Enabled = false;
            deleteNodeButton.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            deleteNodeButton.Location = new Point(4, 64);
            deleteNodeButton.Margin = new Padding(4);
            deleteNodeButton.Name = "deleteNodeButton";
            deleteNodeButton.Size = new Size(275, 52);
            deleteNodeButton.TabIndex = 2;
            deleteNodeButton.Text = "Sterge";
            deleteNodeButton.UseVisualStyleBackColor = true;
            deleteNodeButton.Click += DeleteNodeButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(359, 9);
            label1.Name = "label1";
            label1.Size = new Size(614, 32);
            label1.TabIndex = 4;
            label1.Text = "Proiect Medii si Platforme de Dezvoltare Avansate 2024";
            // 
            // treeView
            // 
            treeView.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            treeView.Location = new Point(12, 285);
            treeView.Name = "treeView";
            treeView.Size = new Size(1307, 399);
            treeView.TabIndex = 5;
            treeView.TabStop = false;
            treeView.AfterSelect += TreeView_AfterSelect;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.Controls.Add(addNodeButton);
            flowLayoutPanel.Controls.Add(deleteNodeButton);
            flowLayoutPanel.Controls.Add(searchNodeButton);
            flowLayoutPanel.Controls.Add(modifyNodeButton);
            flowLayoutPanel.Controls.Add(controlsPanel);
            flowLayoutPanel.Controls.Add(button1);
            flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel.Location = new Point(12, 44);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new Size(1307, 243);
            flowLayoutPanel.TabIndex = 6;
            // 
            // searchNodeButton
            // 
            searchNodeButton.Enabled = false;
            searchNodeButton.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            searchNodeButton.Location = new Point(4, 124);
            searchNodeButton.Margin = new Padding(4);
            searchNodeButton.Name = "searchNodeButton";
            searchNodeButton.Size = new Size(275, 52);
            searchNodeButton.TabIndex = 3;
            searchNodeButton.Text = "Cauta";
            searchNodeButton.UseVisualStyleBackColor = true;
            searchNodeButton.Click += SearchNodeButton_Click;
            // 
            // modifyNodeButton
            // 
            modifyNodeButton.Enabled = false;
            modifyNodeButton.Font = new Font("Segoe UI", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            modifyNodeButton.Location = new Point(3, 183);
            modifyNodeButton.Name = "modifyNodeButton";
            modifyNodeButton.Size = new Size(275, 52);
            modifyNodeButton.TabIndex = 6;
            modifyNodeButton.Text = "Modifica";
            modifyNodeButton.UseVisualStyleBackColor = true;
            modifyNodeButton.Click += ModifyNodeButton_Click;
            // 
            // controlsPanel
            // 
            controlsPanel.Controls.Add(nodeNameTextBox);
            controlsPanel.Controls.Add(label2);
            controlsPanel.Location = new Point(286, 3);
            controlsPanel.Name = "controlsPanel";
            controlsPanel.Size = new Size(1021, 232);
            controlsPanel.TabIndex = 4;
            // 
            // nodeNameTextBox
            // 
            nodeNameTextBox.Enabled = false;
            nodeNameTextBox.Location = new Point(363, 118);
            nodeNameTextBox.Name = "nodeNameTextBox";
            nodeNameTextBox.Size = new Size(349, 29);
            nodeNameTextBox.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlDarkDark;
            label2.Location = new Point(363, 88);
            label2.Name = "label2";
            label2.Size = new Size(349, 25);
            label2.TabIndex = 3;
            label2.Text = "Introdu noua denumire a nodului mai jos";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            button1.Location = new Point(1314, 4);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(275, 52);
            button1.TabIndex = 5;
            button1.Text = "Cauta Produs";
            button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1332, 696);
            Controls.Add(flowLayoutPanel);
            Controls.Add(treeView);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 12F);
            Margin = new Padding(4);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Aplicatie MPDA";
            Load += MainForm_Load;
            flowLayoutPanel.ResumeLayout(false);
            controlsPanel.ResumeLayout(false);
            controlsPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button addNodeButton;
        private Button deleteNodeButton;
        private Label label1;
        private TreeView treeView;
        private FlowLayoutPanel flowLayoutPanel;
        private Panel controlsPanel;
        private Button button1;
        private Button searchNodeButton;
        private Button modifyNodeButton;
        private TextBox nodeNameTextBox;
        private Label label2;
    }
}
