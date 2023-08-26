using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace DSD_WinformsApp.View
{
    partial class DocumentMainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentMainView));
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            buttonManageUsers = new Button();
            buttonDocument = new Button();
            buttonHome = new Button();
            panelDocumentButton = new Panel();
            iconNext = new PictureBox();
            iconBack = new PictureBox();
            comboBoxCategoryDropdown = new ComboBox();
            comboBoxCategory = new ComboBox();
            textBoxSearchBar = new TextBox();
            pictureBox1 = new PictureBox();
            buttonSignOut = new Button();
            panelManageUsers = new Panel();
            dataGridViewManageUsers = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panelDocumentButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconNext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconBack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelManageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewManageUsers).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(20, 225);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 33;
            dataGridView1.Size = new Size(1125, 366);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(11, 36, 71);
            panel1.Controls.Add(buttonManageUsers);
            panel1.Controls.Add(buttonDocument);
            panel1.Controls.Add(buttonHome);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 1024);
            panel1.TabIndex = 1;
            // 
            // buttonManageUsers
            // 
            buttonManageUsers.BackColor = Color.FromArgb(87, 108, 188);
            buttonManageUsers.Cursor = Cursors.Hand;
            buttonManageUsers.FlatStyle = FlatStyle.Flat;
            buttonManageUsers.Location = new Point(8, 240);
            buttonManageUsers.Name = "buttonManageUsers";
            buttonManageUsers.Size = new Size(235, 35);
            buttonManageUsers.TabIndex = 2;
            buttonManageUsers.Text = "Manage Users";
            buttonManageUsers.UseVisualStyleBackColor = false;
            buttonManageUsers.Click += buttonManageUsers_Click;
            // 
            // buttonDocument
            // 
            buttonDocument.BackColor = Color.FromArgb(87, 108, 188);
            buttonDocument.Cursor = Cursors.Hand;
            buttonDocument.FlatStyle = FlatStyle.Flat;
            buttonDocument.Location = new Point(8, 180);
            buttonDocument.Name = "buttonDocument";
            buttonDocument.Size = new Size(235, 35);
            buttonDocument.TabIndex = 1;
            buttonDocument.Text = "Document";
            buttonDocument.UseVisualStyleBackColor = false;
            buttonDocument.Click += buttonDocument_Click;
            // 
            // buttonHome
            // 
            buttonHome.BackColor = Color.FromArgb(87, 108, 188);
            buttonHome.Cursor = Cursors.Hand;
            buttonHome.FlatStyle = FlatStyle.Flat;
            buttonHome.Location = new Point(8, 120);
            buttonHome.Name = "buttonHome";
            buttonHome.Size = new Size(235, 35);
            buttonHome.TabIndex = 0;
            buttonHome.Text = "Home";
            buttonHome.UseVisualStyleBackColor = false;
            buttonHome.Click += button1_Click;
            // 
            // panelDocumentButton
            // 
            panelDocumentButton.BackColor = Color.FromArgb(165, 215, 232);
            panelDocumentButton.Controls.Add(iconNext);
            panelDocumentButton.Controls.Add(iconBack);
            panelDocumentButton.Controls.Add(comboBoxCategoryDropdown);
            panelDocumentButton.Controls.Add(comboBoxCategory);
            panelDocumentButton.Controls.Add(textBoxSearchBar);
            panelDocumentButton.Location = new Point(250, 80);
            panelDocumentButton.Name = "panelDocumentButton";
            panelDocumentButton.Size = new Size(1195, 890);
            panelDocumentButton.TabIndex = 2;
            // 
            // iconNext
            // 
            iconNext.Image = Properties.Resources.next;
            iconNext.Location = new Point(1108, 649);
            iconNext.Name = "iconNext";
            iconNext.Size = new Size(36, 36);
            iconNext.SizeMode = PictureBoxSizeMode.Zoom;
            iconNext.TabIndex = 7;
            iconNext.TabStop = false;
            iconNext.Click += pictureBox3_Click;
            // 
            // iconBack
            // 
            iconBack.Image = Properties.Resources.back;
            iconBack.Location = new Point(1043, 649);
            iconBack.Name = "iconBack";
            iconBack.Size = new Size(36, 36);
            iconBack.SizeMode = PictureBoxSizeMode.Zoom;
            iconBack.TabIndex = 6;
            iconBack.TabStop = false;
            // 
            // comboBoxCategoryDropdown
            // 
            comboBoxCategoryDropdown.FormattingEnabled = true;
            comboBoxCategoryDropdown.Location = new Point(890, 130);
            comboBoxCategoryDropdown.Name = "comboBoxCategoryDropdown";
            comboBoxCategoryDropdown.Size = new Size(250, 33);
            comboBoxCategoryDropdown.TabIndex = 5;
            comboBoxCategoryDropdown.Text = "All Categories";
            comboBoxCategoryDropdown.SelectedIndexChanged += comboBoxCategoryDropdown_SelectedIndexChanged;
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new Point(1200, 180);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new Size(182, 33);
            comboBoxCategory.TabIndex = 4;
            // 
            // textBoxSearchBar
            // 
            textBoxSearchBar.Location = new Point(600, 130);
            textBoxSearchBar.Name = "textBoxSearchBar";
            textBoxSearchBar.PlaceholderText = "Search by Filename";
            textBoxSearchBar.Size = new Size(250, 31);
            textBoxSearchBar.TabIndex = 3;
            textBoxSearchBar.TextChanged += textBoxSearchBar_TextChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources.plus;
            pictureBox1.Location = new Point(20, 130);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 40);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // buttonSignOut
            // 
            buttonSignOut.BackColor = Color.FromArgb(218, 11, 11);
            buttonSignOut.Location = new Point(1293, 25);
            buttonSignOut.Name = "buttonSignOut";
            buttonSignOut.Size = new Size(100, 40);
            buttonSignOut.TabIndex = 3;
            buttonSignOut.Text = "Sign Out";
            buttonSignOut.UseVisualStyleBackColor = false;
            // 
            // panelManageUsers
            // 
            panelManageUsers.BackColor = Color.FromArgb(165, 215, 232);
            panelManageUsers.Controls.Add(dataGridViewManageUsers);
            panelManageUsers.Location = new Point(250, 80);
            panelManageUsers.Name = "panelManageUsers";
            panelManageUsers.Size = new Size(1195, 890);
            panelManageUsers.TabIndex = 4;
            // 
            // dataGridViewManageUsers
            // 
            dataGridViewManageUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewManageUsers.Location = new Point(20, 225);
            dataGridViewManageUsers.Name = "dataGridViewManageUsers";
            dataGridViewManageUsers.RowHeadersVisible = false;
            dataGridViewManageUsers.RowHeadersWidth = 62;
            dataGridViewManageUsers.RowTemplate.Height = 33;
            dataGridViewManageUsers.Size = new Size(1125, 366);
            dataGridViewManageUsers.TabIndex = 0;
            // 
            // DocumentMainView
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1418, 968);
            Controls.Add(buttonSignOut);
            Controls.Add(panel1);
            Controls.Add(panelDocumentButton);
            Controls.Add(dataGridView1);
            Controls.Add(panelManageUsers);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DocumentMainView";
            Text = "Main Page";
            Load += DocumentView_Load_1;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panelDocumentButton.ResumeLayout(false);
            panelDocumentButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconNext).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconBack).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelManageUsers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewManageUsers).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public DataGridView dataGridView1;
        private Panel panel1;
        private Button buttonDocument;
        private Button buttonHome;
        private Button buttonManageUsers;
        private PictureBox pictureBox1;
        private ComboBox comboBoxCategory;
        private TextBox textBoxSearchBar;
        private ComboBox comboBoxCategoryDropdown;
        private Panel panelDocumentButton;
        private Button buttonSignOut;
        private Panel panelManageUsers;
        private DataGridView dataGridViewManageUsers;
        private PictureBox iconNext;
        private PictureBox iconBack;
    }
}