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
            labelAppName = new Label();
            buttonManageUsers = new Button();
            buttonDocument = new Button();
            buttonHome = new Button();
            panelDocumentButton = new Panel();
            labelDocumentPagination = new Label();
            iconNext = new PictureBox();
            iconBack = new PictureBox();
            comboBoxCategoryDropdown = new ComboBox();
            comboBoxCategory = new ComboBox();
            textBoxSearchBar = new TextBox();
            pictureBox1 = new PictureBox();
            buttonSignOut = new Button();
            panelManageUsers = new Panel();
            labelUsersPagination = new Label();
            comboBox_JobCategory = new ComboBox();
            textBoxUsersSearchBox = new TextBox();
            pictureBoxUsersNextIcon = new PictureBox();
            pictureBoxUsersBackIcon = new PictureBox();
            dataGridViewManageUsers = new DataGridView();
            panelUserDetails = new Panel();
            panelUserDetails2 = new Panel();
            checkBoxEnableAdmin = new CheckBox();
            textBoxUserEmailAdd = new TextBox();
            textBoxUserJobTitle = new TextBox();
            textBoxUserLastName = new TextBox();
            textBoxUserFirstName = new TextBox();
            textBoxID = new TextBox();
            pictureBoxUserProfile = new PictureBox();
            labelUserId = new Label();
            labelFirstName = new Label();
            labelLastName = new Label();
            labelJobTitle = new Label();
            labelEmailAddress = new Label();
            panelHome = new Panel();
            labelHomePage = new Label();
            labelHomePageUserLogin = new Label();
            labelFooter = new Label();

            buttonUsersDetailSave = new CustomButton(ColorTranslator.FromHtml("#05982E"), SystemColors.Control);
            buttonCloseUser = new CustomButton(ColorTranslator.FromHtml("#DA0B0B"), SystemColors.Control);
            buttonEditUser = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);

            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            panelDocumentButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconNext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconBack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelManageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUsersNextIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUsersBackIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewManageUsers).BeginInit();
            panelUserDetails.SuspendLayout();
            panelUserDetails2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUserProfile).BeginInit();
            panelHome.SuspendLayout();
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
            dataGridView1.RowTemplate.Resizable = DataGridViewTriState.False;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.Size = new Size(1125, 366);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(11, 36, 71);
            panel1.Controls.Add(labelAppName);
            panel1.Controls.Add(buttonManageUsers);
            panel1.Controls.Add(buttonDocument);
            panel1.Controls.Add(buttonHome);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 1024);
            panel1.TabIndex = 1;
            // 
            // labelAppName
            // 
            labelAppName.AutoSize = true;
            labelAppName.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold, GraphicsUnit.Point);
            labelAppName.ForeColor = SystemColors.HighlightText;
            labelAppName.Location = new Point(24, 17);
            labelAppName.Name = "labelAppName";
            labelAppName.Size = new Size(199, 30);
            labelAppName.TabIndex = 3;
            labelAppName.Text = "Document System ";
            // 
            // buttonManageUsers
            // 
            buttonManageUsers.BackColor = Color.FromArgb(87, 108, 188);
            buttonManageUsers.Cursor = Cursors.Hand;
            buttonManageUsers.FlatStyle = FlatStyle.Flat;
            buttonManageUsers.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonManageUsers.ForeColor = SystemColors.MenuBar;
            buttonManageUsers.Location = new Point(8, 240);
            buttonManageUsers.Name = "buttonManageUsers";
            buttonManageUsers.Size = new Size(235, 38);
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
            buttonDocument.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonDocument.ForeColor = SystemColors.MenuBar;
            buttonDocument.Location = new Point(8, 180);
            buttonDocument.Name = "buttonDocument";
            buttonDocument.Size = new Size(235, 38);
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
            buttonHome.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonHome.ForeColor = SystemColors.MenuBar;
            buttonHome.Location = new Point(8, 120);
            buttonHome.Name = "buttonHome";
            buttonHome.Size = new Size(235, 38);
            buttonHome.TabIndex = 0;
            buttonHome.Text = "Home";
            buttonHome.UseVisualStyleBackColor = false;
            buttonHome.Click += buttonHome_Click;
            // 
            // panelDocumentButton
            // 
            panelDocumentButton.BackColor = Color.FromArgb(165, 215, 232);
            panelDocumentButton.Controls.Add(labelDocumentPagination);
            panelDocumentButton.Controls.Add(iconNext);
            panelDocumentButton.Controls.Add(iconBack);
            panelDocumentButton.Controls.Add(comboBoxCategoryDropdown);
            panelDocumentButton.Controls.Add(comboBoxCategory);
            panelDocumentButton.Controls.Add(textBoxSearchBar);
            panelDocumentButton.Location = new Point(250, 80);
            panelDocumentButton.Name = "panelDocumentButton";
            panelDocumentButton.Size = new Size(1195, 830);
            panelDocumentButton.TabIndex = 2;
            // 
            // labelDocumentPagination
            // 
            labelDocumentPagination.AutoSize = true;
            labelDocumentPagination.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelDocumentPagination.Location = new Point(995, 657);
            labelDocumentPagination.Name = "labelDocumentPagination";
            labelDocumentPagination.Size = new Size(59, 25);
            labelDocumentPagination.TabIndex = 8;
            labelDocumentPagination.Text = "label1";
            // 
            // iconNext
            // 
            iconNext.Cursor = Cursors.Hand;
            iconNext.Image = Properties.Resources.next;
            iconNext.Location = new Point(1110, 650);
            iconNext.Name = "iconNext";
            iconNext.Size = new Size(36, 36);
            iconNext.SizeMode = PictureBoxSizeMode.Zoom;
            iconNext.TabIndex = 7;
            iconNext.TabStop = false;
            iconNext.Click += pictureBox3_Click;
            // 
            // iconBack
            // 
            iconBack.Cursor = Cursors.Hand;
            iconBack.Image = Properties.Resources.back;
            iconBack.Location = new Point(955, 650);
            iconBack.Name = "iconBack";
            iconBack.Size = new Size(36, 36);
            iconBack.SizeMode = PictureBoxSizeMode.Zoom;
            iconBack.TabIndex = 6;
            iconBack.TabStop = false;
            iconBack.Click += iconBack_Click;
            // 
            // comboBoxCategoryDropdown
            // 
            comboBoxCategoryDropdown.FormattingEnabled = true;
            comboBoxCategoryDropdown.Location = new Point(890, 130);
            comboBoxCategoryDropdown.Name = "comboBoxCategoryDropdown";
            comboBoxCategoryDropdown.Size = new Size(250, 33);
            comboBoxCategoryDropdown.TabIndex = 5;
            comboBoxCategoryDropdown.Text = "All Category Options";
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
            buttonSignOut.Cursor = Cursors.Hand;
            buttonSignOut.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            buttonSignOut.Location = new Point(1280, 25);
            buttonSignOut.Name = "buttonSignOut";
            buttonSignOut.Size = new Size(110, 40);
            buttonSignOut.TabIndex = 3;
            buttonSignOut.Text = "Sign Out";
            buttonSignOut.UseVisualStyleBackColor = false;
            buttonSignOut.Click += buttonSignOut_Click;
            // 
            // panelManageUsers
            // 
            panelManageUsers.BackColor = Color.FromArgb(165, 215, 232);
            panelManageUsers.Controls.Add(labelUsersPagination);
            panelManageUsers.Controls.Add(comboBox_JobCategory);
            panelManageUsers.Controls.Add(textBoxUsersSearchBox);
            panelManageUsers.Controls.Add(pictureBoxUsersNextIcon);
            panelManageUsers.Controls.Add(pictureBoxUsersBackIcon);
            panelManageUsers.Controls.Add(dataGridViewManageUsers);
            panelManageUsers.Location = new Point(250, 80);
            panelManageUsers.Name = "panelManageUsers";
            panelManageUsers.Size = new Size(1195, 830);
            panelManageUsers.TabIndex = 4;
            // 
            // labelUsersPagination
            // 
            labelUsersPagination.AutoSize = true;
            labelUsersPagination.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelUsersPagination.Location = new Point(996, 657);
            labelUsersPagination.Name = "labelUsersPagination";
            labelUsersPagination.Size = new Size(59, 25);
            labelUsersPagination.TabIndex = 5;
            labelUsersPagination.Text = "label1";
            // 
            // comboBox_JobCategory
            // 
            comboBox_JobCategory.FormattingEnabled = true;
            comboBox_JobCategory.Location = new Point(890, 169);
            comboBox_JobCategory.Name = "comboBox_JobCategory";
            comboBox_JobCategory.Size = new Size(250, 33);
            comboBox_JobCategory.TabIndex = 4;
            comboBox_JobCategory.Visible = false;
            comboBox_JobCategory.SelectedIndexChanged += comboBox_JobCategory_SelectedIndexChanged;
            // 
            // textBoxUsersSearchBox
            // 
            textBoxUsersSearchBox.Location = new Point(891, 130);
            textBoxUsersSearchBox.Name = "textBoxUsersSearchBox";
            textBoxUsersSearchBox.PlaceholderText = "Search User Name";
            textBoxUsersSearchBox.Size = new Size(250, 31);
            textBoxUsersSearchBox.TabIndex = 3;
            textBoxUsersSearchBox.TextChanged += textBoxUsersSearchBox_TextChanged;
            // 
            // pictureBoxUsersNextIcon
            // 
            pictureBoxUsersNextIcon.Cursor = Cursors.Hand;
            pictureBoxUsersNextIcon.Image = Properties.Resources.next;
            pictureBoxUsersNextIcon.Location = new Point(1110, 650);
            pictureBoxUsersNextIcon.Name = "pictureBoxUsersNextIcon";
            pictureBoxUsersNextIcon.Size = new Size(36, 36);
            pictureBoxUsersNextIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxUsersNextIcon.TabIndex = 2;
            pictureBoxUsersNextIcon.TabStop = false;
            pictureBoxUsersNextIcon.Click += pictureBoxUsersNextIcon_Click;
            // 
            // pictureBoxUsersBackIcon
            // 
            pictureBoxUsersBackIcon.Cursor = Cursors.Hand;
            pictureBoxUsersBackIcon.Image = Properties.Resources.back;
            pictureBoxUsersBackIcon.Location = new Point(955, 650);
            pictureBoxUsersBackIcon.Name = "pictureBoxUsersBackIcon";
            pictureBoxUsersBackIcon.Size = new Size(36, 36);
            pictureBoxUsersBackIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxUsersBackIcon.TabIndex = 1;
            pictureBoxUsersBackIcon.TabStop = false;
            pictureBoxUsersBackIcon.Click += pictureBoxUsersBackIcon_Click;
            // 
            // dataGridViewManageUsers
            // 
            dataGridViewManageUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewManageUsers.Cursor = Cursors.Hand;
            dataGridViewManageUsers.Location = new Point(20, 225);
            dataGridViewManageUsers.Name = "dataGridViewManageUsers";
            dataGridViewManageUsers.RowHeadersVisible = false;
            dataGridViewManageUsers.RowHeadersWidth = 62;
            dataGridViewManageUsers.RowTemplate.Height = 33;
            dataGridViewManageUsers.ScrollBars = ScrollBars.None;
            dataGridViewManageUsers.Size = new Size(1125, 366);
            dataGridViewManageUsers.TabIndex = 0;
            // 
            // panelUserDetails
            // 
            panelUserDetails.BackColor = Color.FromArgb(165, 215, 232);
            panelUserDetails.Controls.Add(panelUserDetails2);
            panelUserDetails.Location = new Point(250, 80);
            panelUserDetails.Name = "panelUserDetails";
            panelUserDetails.Size = new Size(1195, 830);
            panelUserDetails.TabIndex = 0;
            // 
            // panelUserDetails2
            // 
            panelUserDetails2.BackColor = Color.White;
            panelUserDetails2.Controls.Add(buttonEditUser);
            panelUserDetails2.Controls.Add(buttonCloseUser);
            panelUserDetails2.Controls.Add(buttonUsersDetailSave);
            panelUserDetails2.Controls.Add(checkBoxEnableAdmin);
            panelUserDetails2.Controls.Add(textBoxUserEmailAdd);
            panelUserDetails2.Controls.Add(textBoxUserJobTitle);
            panelUserDetails2.Controls.Add(textBoxUserLastName);
            panelUserDetails2.Controls.Add(textBoxUserFirstName);
            panelUserDetails2.Controls.Add(textBoxID);
            panelUserDetails2.Controls.Add(pictureBoxUserProfile);
            panelUserDetails2.Controls.Add(labelUserId);
            panelUserDetails2.Controls.Add(labelFirstName);
            panelUserDetails2.Controls.Add(labelLastName);
            panelUserDetails2.Controls.Add(labelJobTitle);
            panelUserDetails2.Controls.Add(labelEmailAddress);
            panelUserDetails2.Location = new Point(80, 50);
            panelUserDetails2.Name = "panelUserDetails2";
            panelUserDetails2.Size = new Size(1010, 750);
            panelUserDetails2.TabIndex = 6;
            // 
            // buttonEditUser
            // 
            buttonEditUser.BackColor = Color.FromArgb(165, 215, 232);
            buttonEditUser.Location = new Point(768, 607);
            buttonEditUser.Name = "buttonEditUser";
            buttonEditUser.Size = new Size(112, 34);
            buttonEditUser.TabIndex = 21;
            buttonEditUser.Text = "Edit";
            buttonEditUser.UseVisualStyleBackColor = true;
            buttonEditUser.Click += buttonEditUser_Click;
            // 
            // buttonCloseUser
            // 
            buttonCloseUser.BackColor = Color.FromArgb(218, 11, 11);
            buttonCloseUser.Location = new Point(633, 607);
            buttonCloseUser.Name = "buttonCloseUser";
            buttonCloseUser.Size = new Size(112, 34);
            buttonCloseUser.TabIndex = 20;
            buttonCloseUser.Text = "Close";
            buttonCloseUser.UseVisualStyleBackColor = true;
            buttonCloseUser.Click += buttonCloseUser_Click;
            // 
            // buttonUsersDetailSave
            // 
            buttonUsersDetailSave.BackColor = Color.FromArgb(5, 152, 46);
            buttonUsersDetailSave.Enabled = false;
            buttonUsersDetailSave.Location = new Point(499, 607);
            buttonUsersDetailSave.Name = "buttonUsersDetailSave";
            buttonUsersDetailSave.Size = new Size(112, 34);
            buttonUsersDetailSave.TabIndex = 19;
            buttonUsersDetailSave.Text = "Save";
            buttonUsersDetailSave.UseVisualStyleBackColor = true;
            buttonUsersDetailSave.Click += buttonUsersDetailSave_Click;
            // 
            // checkBoxEnableAdmin
            // 
            checkBoxEnableAdmin.AutoSize = true;
            checkBoxEnableAdmin.Location = new Point(499, 302);
            checkBoxEnableAdmin.Name = "checkBoxEnableAdmin";
            checkBoxEnableAdmin.Size = new Size(148, 29);
            checkBoxEnableAdmin.TabIndex = 18;
            checkBoxEnableAdmin.Text = "Enable Admin";
            checkBoxEnableAdmin.UseVisualStyleBackColor = true;
            checkBoxEnableAdmin.CheckedChanged += checkBoxEnableAdmin_CheckedChanged;
            // 
            // textBoxUserEmailAdd
            // 
            textBoxUserEmailAdd.Location = new Point(280, 500);
            textBoxUserEmailAdd.Name = "textBoxUserEmailAdd";
            textBoxUserEmailAdd.Size = new Size(600, 31);
            textBoxUserEmailAdd.TabIndex = 10;
            // 
            // textBoxUserJobTitle
            // 
            textBoxUserJobTitle.Location = new Point(280, 450);
            textBoxUserJobTitle.Name = "textBoxUserJobTitle";
            textBoxUserJobTitle.Size = new Size(600, 31);
            textBoxUserJobTitle.TabIndex = 9;
            // 
            // textBoxUserLastName
            // 
            textBoxUserLastName.Location = new Point(280, 400);
            textBoxUserLastName.Name = "textBoxUserLastName";
            textBoxUserLastName.Size = new Size(600, 31);
            textBoxUserLastName.TabIndex = 8;
            // 
            // textBoxUserFirstName
            // 
            textBoxUserFirstName.Location = new Point(280, 350);
            textBoxUserFirstName.Name = "textBoxUserFirstName";
            textBoxUserFirstName.Size = new Size(600, 31);
            textBoxUserFirstName.TabIndex = 7;
            // 
            // textBoxID
            // 
            textBoxID.Location = new Point(280, 300);
            textBoxID.Name = "textBoxID";
            textBoxID.Size = new Size(181, 31);
            textBoxID.TabIndex = 6;
            // 
            // pictureBoxUserProfile
            // 
            pictureBoxUserProfile.Image = Properties.Resources.user;
            pictureBoxUserProfile.Location = new Point(80, 40);
            pictureBoxUserProfile.Name = "pictureBoxUserProfile";
            pictureBoxUserProfile.Size = new Size(200, 200);
            pictureBoxUserProfile.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxUserProfile.TabIndex = 0;
            pictureBoxUserProfile.TabStop = false;
            // 
            // labelUserId
            // 
            labelUserId.AutoSize = true;
            labelUserId.Location = new Point(80, 300);
            labelUserId.Name = "labelUserId";
            labelUserId.Size = new Size(34, 25);
            labelUserId.TabIndex = 1;
            labelUserId.Text = "ID:";
            // 
            // labelFirstName
            // 
            labelFirstName.AutoSize = true;
            labelFirstName.Location = new Point(80, 350);
            labelFirstName.Name = "labelFirstName";
            labelFirstName.Size = new Size(93, 25);
            labelFirstName.TabIndex = 2;
            labelFirstName.Text = "Firstname:";
            // 
            // labelLastName
            // 
            labelLastName.AutoSize = true;
            labelLastName.Location = new Point(80, 400);
            labelLastName.Name = "labelLastName";
            labelLastName.Size = new Size(91, 25);
            labelLastName.TabIndex = 3;
            labelLastName.Text = "Lastname:";
            // 
            // labelJobTitle
            // 
            labelJobTitle.AutoSize = true;
            labelJobTitle.Location = new Point(80, 450);
            labelJobTitle.Name = "labelJobTitle";
            labelJobTitle.Size = new Size(81, 25);
            labelJobTitle.TabIndex = 4;
            labelJobTitle.Text = "Job Title:";
            // 
            // labelEmailAddress
            // 
            labelEmailAddress.AutoSize = true;
            labelEmailAddress.Location = new Point(80, 500);
            labelEmailAddress.Name = "labelEmailAddress";
            labelEmailAddress.Size = new Size(128, 25);
            labelEmailAddress.TabIndex = 5;
            labelEmailAddress.Text = "Email Address:";
            // 
            // panelHome
            // 
            panelHome.BackColor = Color.FromArgb(165, 215, 232);
            panelHome.Controls.Add(labelHomePage);
            panelHome.Location = new Point(250, 80);
            panelHome.Name = "panelHome";
            panelHome.Size = new Size(1195, 830);
            panelHome.TabIndex = 8;
            // 
            // labelHomePage
            // 
            labelHomePage.AutoSize = true;
            labelHomePage.Font = new Font("Segoe UI", 32F, FontStyle.Regular, GraphicsUnit.Point);
            labelHomePage.Location = new Point(170, 314);
            labelHomePage.Name = "labelHomePage";
            labelHomePage.Size = new Size(832, 86);
            labelHomePage.TabIndex = 0;
            labelHomePage.Text = "Document System Database";
            // 
            // labelHomePageUserLogin
            // 
            labelHomePageUserLogin.AutoSize = true;
            labelHomePageUserLogin.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelHomePageUserLogin.Location = new Point(1012, 33);
            labelHomePageUserLogin.Name = "labelHomePageUserLogin";
            labelHomePageUserLogin.Size = new Size(63, 28);
            labelHomePageUserLogin.TabIndex = 9;
            labelHomePageUserLogin.Text = "label1";
            // 
            // labelFooter
            // 
            labelFooter.AutoSize = true;
            labelFooter.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            labelFooter.Location = new Point(667, 925);
            labelFooter.Name = "labelFooter";
            labelFooter.Size = new Size(381, 28);
            labelFooter.TabIndex = 10;
            labelFooter.Text = "© 2023 TEAM COOP. All Rights Reserved";
            // 
            // DocumentMainView
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1418, 968);
            Controls.Add(labelFooter);
            Controls.Add(labelHomePageUserLogin);
            Controls.Add(buttonSignOut);
            Controls.Add(panel1);
            Controls.Add(panelHome);
            Controls.Add(panelUserDetails);
            Controls.Add(panelDocumentButton);
            Controls.Add(panelManageUsers);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "DocumentMainView";
            Text = "Main Page";
            Load += DocumentView_Load_1;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelDocumentButton.ResumeLayout(false);
            panelDocumentButton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconNext).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconBack).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelManageUsers.ResumeLayout(false);
            panelManageUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUsersNextIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUsersBackIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewManageUsers).EndInit();
            panelUserDetails.ResumeLayout(false);
            panelUserDetails2.ResumeLayout(false);
            panelUserDetails2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxUserProfile).EndInit();
            panelHome.ResumeLayout(false);
            panelHome.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private PictureBox pictureBoxUsersNextIcon;
        private PictureBox pictureBoxUsersBackIcon;
        private TextBox textBoxUsersSearchBox;
        private Label labelRole;
        private Panel panelUserDetails;
        private Panel panelUserDetails2;
        private TextBox textBoxUserEmailAdd;
        private TextBox textBoxUserJobTitle;
        private TextBox textBoxUserLastName;
        private TextBox textBoxUserFirstName;
        private TextBox textBoxID;
        private PictureBox pictureBoxUserProfile;
        private Label labelUserId;
        private Label labelFirstName;
        private Label labelLastName;
        private Label labelJobTitle;
        private Label labelEmailAddress;
        private Panel panelHome;
        private ComboBox comboBox_JobCategory;
        private CheckBox checkBoxEnableAdmin;
        private Label labelHomePage;
        private Label labelHomePageUserLogin;
        private Label labelDocumentPagination;
        private Label labelUsersPagination;
        private Label labelFooter;
        private Label labelAppName;
        private CustomButton buttonEditUser;
        private CustomButton buttonCloseUser;
        private CustomButton buttonUsersDetailSave;
    }
}