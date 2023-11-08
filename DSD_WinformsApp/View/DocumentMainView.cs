using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data;
using DSD_WinformsApp.Infrastructure.Data.Services;
using DSD_WinformsApp.Model;
using DSD_WinformsApp.Presenter;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordApp = Microsoft.Office.Interop.Word.Application;
using ExcelApp = Microsoft.Office.Interop.Excel.Application;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DSD_WinformsApp.View
{
    public partial class DocumentMainView : Form, IDocumentView
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentPresenter _presenter;


        private bool isNewFileUploaded = false;


        // Initial values for search query and category filter
        private string currentSearchQuery = "";
        private string currentFilterCategory = "";
        private bool isVisible;

        // Initial value for user search query
        private string currentSearchUserQuery = "";
        private string currentJobFilter = "";

        public DocumentMainView(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _presenter = new DocumentPresenter(this, _unitOfWork.Documents, _unitOfWork.BackUpFiles, _unitOfWork.Users);



            // Attach the FormClosing event handler
            this.FormClosing += DocumentViewForm_FormClosing;

            ToggleAdminRights(isVisible); // Manage Users button visibility

            timerSearchBar.Interval = 500; // Set the interval for the document searchbar timer
            timerUserSearchBar.Interval = 500; // Set the interval for the user searchbar timer

            // Documents filter events 
            textBoxSearchBar.TextChanged += textBoxSearchBar_TextChanged;
            comboBoxCategoryDropdown.SelectedIndexChanged += comboBoxCategoryDropdown_SelectedIndexChanged;

            // Users filter events
            textBoxUsersSearchBox.TextChanged += textBoxUsersSearchBox_TextChanged;
            comboBox_JobCategory.SelectedIndexChanged += comboBox_JobCategory_SelectedIndexChanged;

            #region Manage Users Events
            // Attach TextChanged event handlers to the relevant text fields
            textBoxUserFirstName.TextChanged += TextBox_TextChanged;
            textBoxUserLastName.TextChanged += TextBox_TextChanged;
            textBoxUserEmailAdd.TextChanged += TextBox_TextChanged;
            textBoxUserJobTitle.TextChanged += TextBox_TextChanged;
            checkBoxEnableAdmin.CheckedChanged += CheckBox_CheckedChanged;

            // Attach CheckedChanged event handler to the checkbox
            checkBoxEnableAdmin.CheckedChanged += CheckBox_CheckedChanged;

            // Store the original state of the checkbox
            originalCheckBoxState = checkBoxEnableAdmin.Checked;

            #endregion
        }

        private async void DocumentView_Load_1(object sender, EventArgs e)
        {
            await _presenter.LoadDocumentsByFilter(currentSearchQuery, currentFilterCategory); // Load the documents by filter using the presenter

            await _presenter.LoadUsersByFilter(currentSearchUserQuery, currentJobFilter); // Load the users from the database using the presenter

            //bool isAdmin = await _presenter.CheckUserAccess(labelHomePageUserLogin.Text); // Check if the logged in user is an admin

            #region Manage Users Properties

            // Create instance for comboBox_JobCategory items
            comboBox_JobCategory.Items.Add("All Job Titles");
            comboBox_JobCategory.Items.Add("Manager");
            comboBox_JobCategory.Items.Add("Staff");
            comboBox_JobCategory.SelectedIndex = 0; // Set the default value to "Select Category"

            // Datagridviewbutton details column
            DataGridViewButtonColumn detailsButtonUserColumn = new DataGridViewButtonColumn();
            detailsButtonUserColumn.Name = "Details";
            detailsButtonUserColumn.Text = "Details";
            detailsButtonUserColumn.Width = 91;
            detailsButtonUserColumn.HeaderText = string.Empty;
            detailsButtonUserColumn.UseColumnTextForButtonValue = true;
            dataGridViewManageUsers.Columns.Add(detailsButtonUserColumn);

            // Datagridviewbutton delete column
            DataGridViewButtonColumn deleteButtonUserColumn = new DataGridViewButtonColumn();
            deleteButtonUserColumn.Name = "Delete";
            deleteButtonUserColumn.Text = "Delete";
            deleteButtonUserColumn.Width = 91;
            deleteButtonUserColumn.HeaderText = string.Empty;
            deleteButtonUserColumn.UseColumnTextForButtonValue = true;
            dataGridViewManageUsers.Columns.Add(deleteButtonUserColumn);

            // Wire up the CellClick event handler
            dataGridViewManageUsers.CellClick += dataGridViewManageUsers_DetailsButton_CellClick;
            dataGridViewManageUsers.CellClick += dataGridViewManageUsers_DeleteButton_CellClick;

            // Set Onclick event for users pagination buttons
            pictureBoxUsersNextIcon.Click += pictureBoxUsersNextIcon_Click;
            pictureBoxUsersBackIcon.Click += pictureBoxUsersBackIcon_Click;

            #endregion

            #region Document Page Properties

            // Set Onclick event for the pagination buttons
            iconNext.Click += pictureBox3_Click;
            iconBack.Click += iconBack_Click;

            panelManageUsers.Visible = false; // Hide the panelManageUsers initially

            // Add controls for panel2
            //pictureBox1.Enabled = isAdmin; // enable for admin user
            panelDocumentButton.Controls.Add(pictureBox1);
            panelDocumentButton.Controls.Add(dataGridView1);

            textBoxSearchBar.Height = 100;
            textBoxSearchBar.Padding = new Padding(5);
            //set default text value for search bar when calling database
            textBoxSearchBar.Text = "";

            // Create instance for comboBoxCategoryDropdown items
            comboBoxCategoryDropdown.Items.Add("All Categories");
            comboBoxCategoryDropdown.Items.Add("Board Resolutions");
            comboBoxCategoryDropdown.Items.Add("Canteen Policies");
            comboBoxCategoryDropdown.Items.Add("COOP Policies");
            comboBoxCategoryDropdown.Items.Add("COOP Article & By Laws");
            comboBoxCategoryDropdown.Items.Add("Minutes of the Meeting");
            comboBoxCategoryDropdown.Items.Add("Regulatory Requirements");
            comboBoxCategoryDropdown.SelectedIndex = 0; // Set the default value to "Select Category"

            // Define the column width from documentmodel
            dataGridView1.Columns["DocumentVersion"].Width = 200;
            dataGridView1.Columns["Filename"].Width = 600;
            dataGridView1.Columns["Category"].Width = 360;
            dataGridView1.Columns["Status"].Width = 185;
            dataGridView1.Columns["CreatedDate"].Width = 186;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["CreatedBy"].Visible = false;
            dataGridView1.Columns["ModifiedBy"].Visible = false;
            dataGridView1.Columns["ModifiedDate"].Visible = false;
            dataGridView1.Columns["Notes"].Visible = false;
            dataGridView1.Columns["FileData"].Visible = false;
            dataGridView1.Columns["FilenameExtension"].Visible = false;

            // Display name for table columns
            dataGridView1.Columns["DocumentVersion"].HeaderText = "DOCUMENT NO.";
            dataGridView1.Columns["Status"].HeaderText = "STATUS";
            dataGridView1.Columns["Filename"].HeaderText = "DOCUMENT TITLE";
            dataGridView1.Columns["Category"].HeaderText = "CATEGORY";
            dataGridView1.Columns["CreatedDate"].HeaderText = "CREATED DATE";

            // Set the font for the header cells
            dataGridView1.Columns["DocumentVersion"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView1.Columns["Status"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView1.Columns["Filename"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView1.Columns["Category"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridView1.Columns["CreatedDate"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // make header text center
            dataGridView1.Columns["DocumentVersion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Status"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Filename"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Category"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["CreatedDate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // force header cell to have color
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#576CBC");


            // Add details button functionality
            DataGridViewButtonColumn detailsColumn = new DataGridViewButtonColumn();
            detailsColumn.Text = "Details";
            detailsColumn.Name = "Details";
            detailsColumn.Width = 93;
            detailsColumn.UseColumnTextForButtonValue = true;
            detailsColumn.HeaderText = string.Empty;
            dataGridView1.Columns.Add(detailsColumn);

            // Wire up the CellClick event handler
            dataGridView1.CellClick += dataGridView1_DetailsButton_CellClick;

            // Set the cursor to hand when hovering over the Details button
            dataGridView1.CellMouseEnter += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    dataGridView1.Cursor = Cursors.Hand;
                }
            };

            #endregion

        }

        public void BindDataMainView(List<DocumentDto> documents) => dataGridView1.DataSource = documents; // Bind documents to dataGridView1

        public void BindDataManageUsers(List<UserCredentialsDto> users) => dataGridViewManageUsers.DataSource = users; // Bind users to dataGridViewManageUsers

        #region Document Page Methods

        private void buttonDocument_Click(object sender, EventArgs e)
        {
            panelManageUsers.Visible = false;
            panelHome.Visible = false;
            panelUserDetails.Visible = false;
            panelDocumentButton.Visible = true;
        }

        private void dataGridView1_DetailsButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Details"].Index)
            {
                DocumentDto selectedDocument = (DocumentDto)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                ShowDocumentDetailsModal(selectedDocument);
            }
        }

        private async void ShowDocumentDetailsModal(DocumentDto selectedDocument)
        {
            // check user access base on labelHomePageUserLogin in document page.
            bool isAdmin = await _presenter.CheckUserAccess(labelHomePageUserLogin.Text);

            // Create a new form to display the document details (modal form).
            DetailsFormView detailsForm = new DetailsFormView();
            detailsForm.Text = "Documents Page";
            detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            detailsForm.StartPosition = FormStartPosition.CenterParent;
            detailsForm.MaximizeBox = false;
            detailsForm.MinimizeBox = false;
            detailsForm.Cursor = Cursors.Default;

            #region Document Details
            // Create the GroupBox
            GroupBox groupBox = new GroupBox();
            groupBox.Text = "Document Details";
            groupBox.AutoSize = false;
            groupBox.Width = detailsForm.ClientSize.Width;
            groupBox.Height = detailsForm.ClientSize.Height;
            groupBox.Location = new Point(20, 80);
            groupBox.Visible = true;

            // Add the GroupBox to the detailsForm
            detailsForm.Controls.Add(groupBox);

            int textBoxWidth = 450; // You can adjust the default width for TextBox controls


            // Create download button
            CustomButton downloadButton = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            downloadButton.Text = "Download";
            downloadButton.Name = "downloadButton";
            downloadButton.Location = new Point(groupBox.Right - (downloadButton.Width + 120), groupBox.Top - 50);
            downloadButton.Height = 40;
            downloadButton.Width = 110;
            downloadButton.Click += (sender, e) =>
            {

                // Show the delete confirmation modal directly in the main form.
                DialogResult result = MessageBox.Show(detailsForm, $"Do you want to delete {selectedDocument.Filename}", "Download Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    downloadButton.Enabled = false;
                    ConfirmDownloadDocument(selectedDocument);
                    downloadButton.Enabled = true; // Enable the button again after the download is complete
                }

            };
            groupBox.Controls.Add(downloadButton);

            // Create delete button
            CustomButton deleteButton = new CustomButton(ColorTranslator.FromHtml("#DA0B0B"), SystemColors.Control);
            deleteButton.Text = "Delete";
            deleteButton.Name = "deleteButton";
            deleteButton.Location = new Point(downloadButton.Left - (downloadButton.Width + 10), downloadButton.Top);
            deleteButton.Height = 40;
            deleteButton.Width = 110;
            deleteButton.Visible = isAdmin;
            deleteButton.Click += async (sender, e) =>
            {
                DialogResult result = MessageBox.Show(detailsForm, $"Do you want to delete {selectedDocument.Filename}?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question); // Show the delete confirmation modal directly in the main form.

                if (result == DialogResult.Yes)
                {
                    await _presenter.DeleteDocumentWithBackups(selectedDocument); // Delete the document and its backups from the database.

                    // Get the current search query and filter category
                    var currentSearchQueryWhenItemDeleted = GetSearchQuery();
                    var currentFilterCategoryWhenItemDeleted = GetFilterCategory();

                    await _presenter.LoadDocumentsByFilter(currentSearchQueryWhenItemDeleted, currentFilterCategoryWhenItemDeleted); // Load the filtered documents again to update the view

                    detailsForm.Close(); // Close the detailsForm
                }
            };
            groupBox.Controls.Add(deleteButton);


            // Create a TextBox for "Filename"
            TextBox filenameTextBox = new TextBox();
            filenameTextBox.Text = selectedDocument.Filename;
            filenameTextBox.ReadOnly = true;
            filenameTextBox.Multiline = true;
            filenameTextBox.Height = 36;
            int filenameTextBoxWidth = textBoxWidth - 130;
            AddRow(groupBox, "Document Title:", filenameTextBox, filenameTextBoxWidth);

            // Create the "Upload File" button and pass the filenameTextBox as a parameter
            CustomButton uploadFileButton = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            uploadFileButton.Text = "Upload File";
            uploadFileButton.Location = new Point(filenameTextBox.Right + 10, filenameTextBox.Top - 5);
            uploadFileButton.Height = filenameTextBox.Height + 6;
            uploadFileButton.Width = 120;
            uploadFileButton.Padding = new Padding(0);
            uploadFileButton.Enabled = false;
            uploadFileButton.Click += (sender, e) => UploadFileButton_Click(sender, e, filenameTextBox);
            groupBox.Controls.Add(uploadFileButton);

            // Create textbox for Document Version
            TextBox documentVersionTextBox = new TextBox();
            documentVersionTextBox.Text = selectedDocument.DocumentVersion;
            documentVersionTextBox.ReadOnly = true;
            documentVersionTextBox.Multiline = true;
            documentVersionTextBox.Height = 36;
            int documentVersionTextBoxWidth = textBoxWidth;
            AddRow(groupBox, "Document Version:", documentVersionTextBox, documentVersionTextBoxWidth);

            // Create the Category ComboBox
            ComboBox categoryComboBox = new ComboBox();
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryComboBox.Items.Add("Board Resolutions");
            categoryComboBox.Items.Add("Canteen Policies");
            categoryComboBox.Items.Add("COOP Policies");
            categoryComboBox.Items.Add("COOP Article & By Laws");
            categoryComboBox.Items.Add("Minutes of the Meeting");
            categoryComboBox.Items.Add("Regulatory Requirements");
            categoryComboBox.Text = selectedDocument.Category.ToString();
            categoryComboBox.Enabled = false;
            categoryComboBox.Height = 36;
            int categoryComboBoxWidth = textBoxWidth;
            AddRow(groupBox, "Category:", categoryComboBox, categoryComboBoxWidth);

            // Create the Status ComboBox
            ComboBox statusComboBox = new ComboBox();
            statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            statusComboBox.Items.Add("New");
            statusComboBox.Items.Add("Revised");
            statusComboBox.Items.Add("Obsolete");
            statusComboBox.SelectedItem = selectedDocument.Status.ToString();
            statusComboBox.Enabled = false;
            statusComboBox.Height = 36;
            int statusComboBoxWidth = textBoxWidth;
            AddRow(groupBox, "Status:", statusComboBox, statusComboBoxWidth);

            // Create a TextBox for the "Created Date" property and set its initial value
            TextBox createdDateTextBox = new TextBox();
            createdDateTextBox.Text = selectedDocument.CreatedDate.ToString("yyyy-MM-dd");
            createdDateTextBox.ReadOnly = true;
            createdDateTextBox.Multiline = true;
            createdDateTextBox.Height = 36;
            int createdDateTextBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Created Date:", createdDateTextBox, createdDateTextBoxWidth);

            // Create a TextBox for the "Created By" property and set its initial value
            TextBox createdByTextBox = new TextBox();
            createdByTextBox.Text = selectedDocument.CreatedBy;
            createdByTextBox.ReadOnly = true;
            createdByTextBox.Multiline = true;
            createdByTextBox.Height = 36;
            int createdByTextBoxWidth = textBoxWidth;
            AddRow(groupBox, "Created By:", createdByTextBox, createdByTextBoxWidth);

            // Create a TextBox for the "Modified By" property and set its initial value
            TextBox modifiedByTextBox = new TextBox();
            modifiedByTextBox.Text = selectedDocument.ModifiedBy;
            modifiedByTextBox.ReadOnly = true;
            modifiedByTextBox.Multiline = true;
            modifiedByTextBox.Height = 36;
            int modifiedByTextBoxWidth = textBoxWidth;
            AddRow(groupBox, "Modified By:", modifiedByTextBox, modifiedByTextBoxWidth);

            // Create a TextBox for the "Modified Date" property and set its initial value
            TextBox modifiedDateTextBox = new TextBox();
            modifiedDateTextBox.Text = selectedDocument.ModifiedDate.ToString("yyyy-MM-dd");
            modifiedDateTextBox.ReadOnly = true;
            modifiedDateTextBox.Multiline = true;
            modifiedDateTextBox.Height = 36;
            int modifiedDateTextBoxWidth = textBoxWidth;
            AddRow(groupBox, "Modified Date:", modifiedDateTextBox, modifiedDateTextBoxWidth);

            // Create a multiline TextBox for the "Notes" property
            TextBox notesTextBox = new TextBox();
            notesTextBox.Text = selectedDocument.Notes;
            notesTextBox.ReadOnly = true;
            notesTextBox.Multiline = true;
            notesTextBox.MaxLength = 200;
            notesTextBox.Height = 100;

            int notesTextBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Notes:", notesTextBox, notesTextBoxWidth);

            // Function to add a row (label + control) to the GroupBox
            void AddRow(GroupBox parent, string labelText, Control control, int controlWidth)
            {
                Label label = new Label();
                label.Text = labelText;
                label.AutoSize = true;

                int labelTop = parent.Controls.Count * 25 + 50;
                label.Location = new Point(50, labelTop);

                int controlLeft = label.Right + 80;
                control.Location = new Point(controlLeft, labelTop);
                control.Width = controlWidth;

                parent.Controls.Add(label);
                parent.Controls.Add(control);
            }

            // Adjust the size of the GroupBox to fit its contents
            int groupBoxWidth = 800;
            int groupBoxHeight = groupBox.Controls.Count * 30 + 30;
            groupBox.Width = groupBoxWidth;
            groupBox.Height = groupBoxHeight;

            #endregion

            #region Document History

            // Create the buttons and add them to the detailsForm
            CustomButton button1 = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            button1.Text = "Details";
            button1.Location = new Point(20, 35); // Adjust the coordinates as needed.
            button1.Height = 40;
            button1.Width = 100;
            detailsForm.Controls.Add(button1);

            CustomButton button2 = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            button2.Text = "History";
            button2.Location = new Point(button1.Right + 10, 35);
            button2.Height = button1.Height;
            button2.Width = button1.Width;
            button2.Visible = isAdmin; // Hide the button if the user is not an admin
            detailsForm.Controls.Add(button2);

            // Create the GroupBox containing the second DataGridView (DataGridView2)
            GroupBox groupBox2 = new GroupBox();
            groupBox2.Text = "Document History";
            groupBox2.AutoSize = true;
            groupBox2.Location = new Point(20, button2.Bottom + 20); // Adjust the position as needed
            groupBox2.Visible = false; // Set the initial visibility to false
            detailsForm.Controls.Add(groupBox2);

            DataGridView dataGridView2 = new DataGridView();
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.ScrollBars = ScrollBars.Horizontal;
            groupBox2.Controls.Add(dataGridView2);


            // Adjust the size of the DetailsFormView to fit the contents including groupBox2
            int groupBox2Width = detailsForm.ClientSize.Width - 50;
            int groupBox2Height = detailsForm.ClientSize.Height - groupBox2.Top - 40;
            groupBox2.Size = new Size(groupBox2Width, groupBox2Height);

            // Binding related backup file data to dataGridView2 based on the selected document's ID
            var relatedBackupFiles = await _presenter.GetRelatedBackupFiles(selectedDocument.Id);
            dataGridView2.DataSource = relatedBackupFiles;


            // Display Columns in datagridview2
            dataGridView2.Columns["DocumentVersion"].Width = 170;
            dataGridView2.Columns["Filename"].Width = 330;
            dataGridView2.Columns["BackupDate"].Width = 120;
            dataGridView2.Columns["Version"].Width = 100;

            dataGridView2.Columns["DocumentVersion"].HeaderText = "Document No.";
            dataGridView2.Columns["Filename"].HeaderText = "Document Title";
            dataGridView2.Columns["BackupDate"].HeaderText = "Date";
            dataGridView2.Columns["Version"].HeaderText = "Version No.";

            dataGridView2.Columns["BackupId"].Visible = false;
            dataGridView2.Columns["OriginalFilePath"].Visible = false;
            dataGridView2.Columns["BackupFilePath"].Visible = false;
            dataGridView2.Columns["Id"].Visible = false;

            // Set the cursor to hand when hovering over the Details button
            dataGridView2.CellMouseEnter += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView2.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    dataGridView2.Cursor = Cursors.Hand;
                }
            };

            // Add download button functionality
            DataGridViewButtonColumn downloadColumn = new DataGridViewButtonColumn();
            downloadColumn.Text = "Download";
            downloadColumn.Name = "";
            downloadColumn.Width = 100;
            downloadColumn.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(downloadColumn);

            // Subscribe to the CellClick event of the DataGridView for download button.
            dataGridView2.CellClick += (sender, e) =>
            {
                // Check if the clicked cell is in the "Download" button column
                if (e.ColumnIndex == downloadColumn.Index && e.RowIndex >= 0)
                {
                    // Get the BackUpFileDto associated with the clicked row
                    if (dataGridView2.Rows[e.RowIndex].DataBoundItem is BackUpFileDto selectedBackupFile)
                    {
                        try
                        {
                            string sourceFilePath = selectedBackupFile.BackupFilePath; // Backup file path

                            string docExtension = Path.GetExtension(sourceFilePath); // Get the file extension
                            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); // Downloads folder path
                            string destinationFilePath = Path.Combine(downloadsPath, "Downloads", selectedBackupFile.Filename + docExtension);

                            File.Copy(sourceFilePath, destinationFilePath, true); // Copy the file to the Downloads folder

                            // Show a message to indicate the download completion
                            MessageBox.Show(detailsForm, $"{selectedBackupFile.Filename} downloaded successfully!", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(detailsForm, $"Error downloading the file: {ex.Message}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            };

            // Add delete button functionality
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Text = "Delete";
            deleteColumn.Name = "";
            deleteColumn.Width = 100;
            deleteColumn.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(deleteColumn);

            //Subscribe to the CellClick event of the DataGridView for delete button.
            dataGridView2.CellClick += async (sender, e) =>
            {
                // Check if the clicked cell is in the "Delete" button column
                if (e.ColumnIndex == deleteColumn.Index && e.RowIndex >= 0)
                {
                    // Get the BackUpFileDto associated with the clicked row
                    if (dataGridView2.Rows[e.RowIndex].DataBoundItem is BackUpFileDto selectedBackupFile)
                    {
                        // Show a confirmation message before deleting the file
                        DialogResult result = MessageBox.Show(detailsForm, $"Do you want to delete {selectedBackupFile.Filename}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                await _presenter.DeleteBackUpFile(selectedBackupFile); // Delete the backup file

                                // Refresh the DataGridView
                                relatedBackupFiles.Remove(selectedBackupFile);
                                dataGridView2.DataSource = null;
                                dataGridView2.DataSource = relatedBackupFiles;

                                // DisplayIndex beginning from DocumentVersion
                                dataGridView2.Columns["DocumentVersion"].DisplayIndex = 0;
                                dataGridView2.Columns["Filename"].DisplayIndex = 1;
                                dataGridView2.Columns["BackupDate"].DisplayIndex = 2;
                                dataGridView2.Columns["Version"].DisplayIndex = 3;

                                // Display Columns in datagridview2
                                dataGridView2.Columns["DocumentVersion"].Width = 170;
                                dataGridView2.Columns["Filename"].Width = 330;
                                dataGridView2.Columns["BackupDate"].Width = 120;
                                dataGridView2.Columns["Version"].Width = 100;

                                dataGridView2.Columns["DocumentVersion"].HeaderText = "Document No.";
                                dataGridView2.Columns["Filename"].HeaderText = "Document Title";
                                dataGridView2.Columns["BackupDate"].HeaderText = "Date";
                                dataGridView2.Columns["Version"].HeaderText = "Version No.";

                                dataGridView2.Columns["BackupId"].Visible = false;
                                dataGridView2.Columns["OriginalFilePath"].Visible = false;
                                dataGridView2.Columns["BackupFilePath"].Visible = false;
                                dataGridView2.Columns["Id"].Visible = false;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(detailsForm, $"Error deleting the file: {ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            };

            #endregion

            #region Details Form Buttons
            // Create the Edit button
            CustomButton editButton = new CustomButton(ColorTranslator.FromHtml("#576CBC"), SystemColors.Control);
            editButton.Text = "Edit";
            editButton.Name = "editButton";
            editButton.Location = new Point(groupBox.Right - editButton.Width, groupBox.Bottom + 10);
            editButton.Height = 40;
            editButton.Width = 80;
            editButton.Enabled = isAdmin; // Enable for admin only
            editButton.Click += EditButton_Click;
            detailsForm.Controls.Add(editButton);

            // Create the Close button
            CustomButton closeButton = new CustomButton(ColorTranslator.FromHtml("#DA0B0B"), SystemColors.Control);
            closeButton.Text = "Close";
            closeButton.Name = "closeButton";
            closeButton.Location = new Point(editButton.Left - 10 - closeButton.Width, groupBox.Bottom + 10);
            closeButton.Height = editButton.Height;
            closeButton.Width = editButton.Width;
            closeButton.Click += CloseButton_Click;
            detailsForm.Controls.Add(closeButton);


            // Create the Save button using the custom button class
            CustomButton saveButton = new CustomButton(ColorTranslator.FromHtml("#05982E"), SystemColors.Control);
            saveButton.Text = "Save";
            saveButton.Name = "saveButton";
            saveButton.Location = new Point(closeButton.Left - 10 - saveButton.Width, closeButton.Top);
            saveButton.Height = closeButton.Height;
            saveButton.Width = closeButton.Width;
            saveButton.Enabled = false; // Disable the Save button initially

            // Create a dictionary to store the original values of the TextBoxes
            var originalTextBoxValues = new Dictionary<TextBox, string>();

            // Save button click event
            saveButton.Click += async (sender, e) =>
            {
                // Check if a file has been uploaded
                string? filePath = filenameTextBox.Tag as string;

                // Read the file data from the selected file if a new file has been uploaded
                byte[] fileDataBytes = isNewFileUploaded ? File.ReadAllBytes(filePath) : selectedDocument.FileData;

                // Get the modified data from the TextBoxes and ComboBoxes
                string documentVersion = documentVersionTextBox.Text;
                string filename = filenameTextBox.Text;
                string filenameExtension = selectedDocument.FilenameExtension; // Get filename extension from db
                string category = categoryComboBox.Text;
                string status = statusComboBox.Text;
                DateTime createdDate = DateTime.Parse(createdDateTextBox.Text);
                string createdBy = createdByTextBox.Text;
                string modifiedBy = labelHomePageUserLogin.Text;
                DateTime modifiedDate = DateTime.Parse(modifiedDateTextBox.Text);
                string notes = notesTextBox.Text;

                // Check if the file name has been changed
                if (filename != Path.GetFileNameWithoutExtension(filePath))
                {
                    filenameTextBox.Text = filename;
                }

                // Create a new DocumentDto with the modified data
                DocumentDto modifiedDocument = new DocumentDto
                {
                    Id = selectedDocument.Id,
                    DocumentVersion = documentVersion,
                    Filename = filename,
                    Category = category,
                    Status = status,
                    CreatedDate = createdDate,
                    CreatedBy = createdBy,
                    ModifiedBy = modifiedBy,
                    ModifiedDate = modifiedDate,
                    Notes = notes,
                    FilenameExtension = filenameExtension
                };

                _presenter.EditDocument(modifiedDocument, fileDataBytes); // Edit the document in the database

                await _presenter.LoadDocumentsByFilter(GetSearchQuery(), GetFilterCategory()); // Load the filtered documents again to update the view

                var result = MessageBox.Show(detailsForm, $"{filenameTextBox.Text} details have been updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show a message to indicate that the document has been updated.                                                                                                                                                   
                if (result == DialogResult.OK)
                {
                    detailsForm.Close(); // Close the detailsForm
                }

                // After saving, make the TextBoxes read-only again
                foreach (Control control in groupBox.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.ReadOnly = true;
                        textBox.TextChanged -= TextBox_TextChanged; // Detach the event handler to stop tracking changes
                    }
                    else if (control is ComboBox comboBox)
                    {
                        comboBox.Enabled = false; // Disable the ComboBox controls
                        comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged; // Detach the event handler to stop tracking changes
                    }
                }

                isNewFileUploaded = false; // Reset the flag

                saveButton.Enabled = false; // Disable the Save button after save
                editButton.Enabled = true; // Re-enable the Edit button after save
            };

            detailsForm.Controls.Add(saveButton);

            // Handle the Edit button click event
            editButton.Click += (sender, e) =>
            {
                // Enable editing of the controls inside the GroupBox.
                foreach (Control control in groupBox.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        // Store the original value of the TextBox
                        originalTextBoxValues[textBox] = textBox.Text;

                        textBox.ReadOnly = false;
                        textBox.TextChanged += TextBox_TextChanged; // Attach the event handler to track changes
                    }

                    else if (control is ComboBox comboBox)
                    {
                        comboBox.Enabled = true; // Enable the ComboBox controls
                        comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged; // Attach the event handler to track changes
                    }
                }

                uploadFileButton.Enabled = true; // Enable the Upload File button
                editButton.Enabled = false; // Disable the Edit button after enabling editing
            };

            // Adjust the size of the detailsForm to fit the GroupBox and its contents
            detailsForm.ClientSize = new Size(groupBox.Right + 40, groupBox.Bottom + 80); // Add some buffer (e.g., 40 pixels) to avoid cutting off any controls.

            // Handle the Click event of Button 1
            button1.Click += (sender, e) =>
            {
                groupBox.Visible = true;
                editButton.Visible = true;
                saveButton.Visible = true;
                closeButton.Visible = true;
            };

            // Handle the Click event of Button 2
            button2.Click += (sender, e) =>
            {
                groupBox.Visible = false;
                groupBox2.Visible = true;
                editButton.Visible = false;
                saveButton.Visible = false;
                closeButton.Visible = false;

            };

            // Handle the Edit button click event
            void EditButton_Click(object? sender, EventArgs e)
            {
                foreach (Control control in groupBox.Controls)
                {
                    // Check if the control is a TextBox and make it editable, except for Created Date and Modified Date
                    if (control is TextBox textBox && textBox != createdDateTextBox && textBox != modifiedDateTextBox)
                    {
                        textBox.ReadOnly = false;
                    }
                }

                // Disable the Edit button after enabling editing
                editButton.Enabled = false;
            }

            // Handle the Close button click event
            void CloseButton_Click(object? sender, EventArgs e)
            {
                detailsForm.Close(); // Close the form when the Close button is clicked.
            }

            void TextBox_TextChanged(object? sender, EventArgs e)
            {
                // Enable the Save button when changes are made in any of the TextBoxes
                saveButton.Enabled = true;

                // Check if the value in the TextBox has been reverted to the original value
                if (sender is TextBox textBox && originalTextBoxValues.ContainsKey(textBox))
                {
                    if (textBox.Text == originalTextBoxValues[textBox])
                    {
                        // If the current value matches the original value, disable the Save button
                        saveButton.Enabled = false;
                    }
                }
            }

            // ComboBox SelectedIndexChanged event handler to track changes
            void ComboBox_SelectedIndexChanged(object? sender, EventArgs e)
            {
                // Enable the Save button when changes are made in any of the ComboBoxes
                saveButton.Enabled = true;
            }

            // Show the detailsForm
            detailsForm.ShowDialog();

            #endregion

        }

        private void ConfirmDownloadDocument(DocumentDto selectedDocument)
        {
            try
            {
                // Get the file data (byte array) from the selectedDocument
                byte[] fileData = selectedDocument.FileData;

                // Distination path in the user's "Downloads" folder
                string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string destinationFilePath = Path.Combine(downloadsPath, "Downloads", Path.ChangeExtension(selectedDocument.Filename, ".pdf"));

                string fileExtension = selectedDocument.FilenameExtension.ToLower();

                if (fileExtension == "docx" || fileExtension == "doc")
                {
                    // Save the file data to a temporary file
                    string tempFilePath = Path.GetTempFileName();
                    File.WriteAllBytes(tempFilePath, selectedDocument.FileData);

                    // If it's a Word document, convert it to PDF using Office Interop
                    Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                    Microsoft.Office.Interop.Word.Document doc = word.Documents.Open(tempFilePath);

                    // Specify the destination file with a ".pdf" extension
                    string pdfFilePath = Path.ChangeExtension(destinationFilePath, ".pdf");

                    doc.SaveAs2(pdfFilePath, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                    doc.Close();
                    word.Quit();

                    // Delete the temporary file
                    File.Delete(tempFilePath);
                }
                else if (fileExtension == "xlsx" || fileExtension == "xls")
                {
                    // Save the file data to a temporary file
                    string tempFilePath = Path.GetTempFileName();
                    File.WriteAllBytes(tempFilePath, selectedDocument.FileData);

                    // If it's an Excel document, convert it to PDF using Office Interop
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Open(tempFilePath);

                    string pdfFilePath = Path.ChangeExtension(destinationFilePath, ".pdf"); // Specify the destination file with a ".pdf" extension

                    wb.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, pdfFilePath);
                    wb.Close();
                    excel.Quit();

                    File.Delete(tempFilePath); // Delete the temporary file
                }
                else
                {
                    // For other file types, simply write the file data to the destination file
                    File.WriteAllBytes(destinationFilePath, fileData);
                }

                MessageBox.Show("Document downloaded successfully!", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading: {ex.Message}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void UploadFileButton_Click(object? sender, EventArgs e, TextBox filenameTextBox)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                string filePath = openFileDialog.FileName; // Get the full path of the selected file
                string filePathFilename = Path.GetFileNameWithoutExtension(filePath);


                // Check if the selected file is different from the current file
                if (!string.Equals(filePathFilename, filenameTextBox.Text, StringComparison.OrdinalIgnoreCase))
                {
                    filenameTextBox.Text = filePathFilename; // Update the TextBox with the selected file name

                    isNewFileUploaded = true; // Set the flag to indicate that a new file has been uploaded
                }
                else
                {
                    MessageBox.Show($"{filenameTextBox.Text} already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Show an error message if the selected file is the same as the current file
                }

                // Disable editing of the TextBox
                filenameTextBox.Enabled = false;
                // Store the file path in the Tag property of the TextBox
                filenameTextBox.Tag = filePath;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // if set combo box to index 0 but when its already at index 0, it will go to index -1
            if (comboBoxCategoryDropdown.SelectedIndex == 0)
            {
                comboBoxCategoryDropdown.SelectedIndex = -1;
            }
            else
            {
                comboBoxCategoryDropdown.SelectedIndex = 0;
            }

            // Clear search bar
            textBoxSearchBar.Text = "";

            using (AddFormView newForm = new AddFormView(_unitOfWork, _presenter))
            {
                newForm.StartPosition = FormStartPosition.CenterParent;
                newForm.ShowDialog();
                comboBoxCategoryDropdown.SelectedIndex = 0;
            }
        }

        public void ShowDocumentView() => this.ShowDialog(); // Show documentMainView form


        private void DocumentViewForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit(); // Exit the application when the form is closed
            }
        }

        #endregion

        private void buttonHome_Click(object sender, EventArgs e)
        {
            panelHome.Visible = true;
            panelDocumentButton.Visible = false;
            panelManageUsers.Visible = false;

        }

        #region Manage Users Methods

        // Event method when details button was clicked
        public void ToggleAdminRights(bool isVisible)
        {
            buttonManageUsers.Visible = isVisible;
            pictureBox1.Visible = isVisible; // Add document icon
            labelDownloadAllDocs.Visible = isVisible;
            linkLabelDownloadAllDocs.Visible = isVisible;
        }

        private async void buttonManageUsers_Click(object sender, EventArgs e)
        {
            var currentUsersSearchQueryWhenItemDeleted = GetSearchUserQuery();
            var currentUsersFilterCategoryWhenItemDeleted = GetFilterUsersCategory();

            await _presenter.LoadUsersByFilter(currentUsersSearchQueryWhenItemDeleted, currentUsersFilterCategoryWhenItemDeleted);


            panelDocumentButton.Visible = false;
            panelHome.Visible = false;
            panelUserDetails.Visible = false;
            panelManageUsers.Visible = true;
            panelManageUsers.Controls.Add(dataGridViewManageUsers);

            // Set the datagridviewManageUsers column properties
            dataGridViewManageUsers.Columns["UserId"].DisplayIndex = 0;
            dataGridViewManageUsers.Columns["Firstname"].DisplayIndex = 1;
            dataGridViewManageUsers.Columns["Lastname"].DisplayIndex = 2;
            dataGridViewManageUsers.Columns["EmailAddress"].DisplayIndex = 3;
            dataGridViewManageUsers.Columns["JobTitle"].DisplayIndex = 4;
            dataGridViewManageUsers.Columns["UserRole"].DisplayIndex = 5;

            // Set the column widths
            dataGridViewManageUsers.Columns["Firstname"].Width = 245;
            dataGridViewManageUsers.Columns["Lastname"].Width = 245;
            dataGridViewManageUsers.Columns["EmailAddress"].Width = 470;
            dataGridViewManageUsers.Columns["JobTitle"].Width = 280;
            dataGridViewManageUsers.Columns["UserRole"].Width = 200;

            dataGridViewManageUsers.Columns["Firstname"].Visible = true;
            dataGridViewManageUsers.Columns["Lastname"].Visible = true;
            dataGridViewManageUsers.Columns["EmailAddress"].Visible = true;
            dataGridViewManageUsers.Columns["JobTitle"].Visible = true;
            dataGridViewManageUsers.Columns["UserRole"].Visible = true;
            dataGridViewManageUsers.Columns["UserId"].Visible = false;
            dataGridViewManageUsers.Columns["CreatedDate"].Visible = false;
            dataGridViewManageUsers.Columns["Password"].Visible = false;
            dataGridViewManageUsers.Columns["ImageData"].Visible = false;
            dataGridViewManageUsers.Columns["Username"].Visible = false;

            // Display name for headertext
            dataGridViewManageUsers.Columns["Firstname"].HeaderText = "FIRST NAME";
            dataGridViewManageUsers.Columns["Lastname"].HeaderText = "LAST NAME";
            dataGridViewManageUsers.Columns["EmailAddress"].HeaderText = "EMAIL ADDRESS";
            dataGridViewManageUsers.Columns["JobTitle"].HeaderText = "JOB TITLE";
            dataGridViewManageUsers.Columns["UserRole"].HeaderText = "USER ROLE";

            // Set header text to center
            dataGridViewManageUsers.Columns["Firstname"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewManageUsers.Columns["Lastname"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewManageUsers.Columns["EmailAddress"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewManageUsers.Columns["JobTitle"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewManageUsers.Columns["UserRole"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Set header text to bold
            dataGridViewManageUsers.Columns["Firstname"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewManageUsers.Columns["Lastname"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewManageUsers.Columns["EmailAddress"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewManageUsers.Columns["JobTitle"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewManageUsers.Columns["UserRole"].HeaderCell.Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Force header cell to have color
            dataGridViewManageUsers.EnableHeadersVisualStyles = false;
            dataGridViewManageUsers.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#576CBC");

        }

        // event when details button was clicked
        private void dataGridViewManageUsers_DetailsButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewManageUsers.Columns["Details"].Index)
            {
                UserCredentialsDto selectedUser = (UserCredentialsDto)dataGridViewManageUsers.Rows[e.RowIndex].DataBoundItem;

                // show panelUserDetails only
                panelUserDetails.Visible = true;
                panelManageUsers.Visible = false;
                panelHome.Visible = false;
                panelDocumentButton.Visible = false;

                // Show the selected user's details in the textboxes
                ShowUserDetails(selectedUser);
            }
        }

        private void ShowUserDetails(UserCredentialsDto selectedUser)
        {
            // Display the selected user's details in the textboxes
            textBoxID.Text = selectedUser.UserId.ToString();
            textBoxID.Enabled = false;

            textBoxUserFirstName.Text = selectedUser.Firstname;
            textBoxUserFirstName.Enabled = false;

            textBoxUserLastName.Text = selectedUser.Lastname;
            textBoxUserLastName.Enabled = false;

            textBoxUserEmailAdd.Text = selectedUser.EmailAddress;
            textBoxUserEmailAdd.Enabled = false;

            textBoxUserJobTitle.Text = selectedUser.JobTitle;
            textBoxUserJobTitle.Enabled = false;

            checkBoxEnableAdmin.Checked = selectedUser.UserRole == UserRole.Admin;
            checkBoxEnableAdmin.Enabled = false;

            // Manage Users button state initially
            buttonUsersDetailSave.Enabled = false;
            buttonUsersDetailSave.BackColor = SystemColors.Control;

            buttonEditUser.Enabled = true;
            buttonEditUser.BackColor = ColorTranslator.FromHtml("#A5D7E8");

            buttonCloseUser.Enabled = true;
            buttonCloseUser.BackColor = ColorTranslator.FromHtml("#DA0B0B");
        }

        private Dictionary<Control, string> originalValues = new Dictionary<Control, string>();

        private void buttonEditUser_Click(object sender, EventArgs e)
        {
            buttonEditUser.Enabled = false; // Disable the Edit button
            buttonCloseUser.Enabled = true; // Disable the Close button

            // Enable editing of the textboxes
            textBoxID.Enabled = false;
            checkBoxEnableAdmin.Enabled = true;
            textBoxUserFirstName.Enabled = true;
            textBoxUserLastName.Enabled = true;
            textBoxUserEmailAdd.Enabled = true;
            textBoxUserJobTitle.Enabled = true;

            // Store the original values of the text fields in the Dictionary
            originalValues.Clear(); // Clear any previous values
            originalValues.Add(textBoxUserFirstName, textBoxUserFirstName.Text);
            originalValues.Add(textBoxUserLastName, textBoxUserLastName.Text);
            originalValues.Add(textBoxUserEmailAdd, textBoxUserEmailAdd.Text);
            originalValues.Add(textBoxUserJobTitle, textBoxUserJobTitle.Text);

            // Store the original state of the checkbox
            originalCheckBoxState = checkBoxEnableAdmin.Checked;
        }


        private async void buttonUsersDetailSave_Click(object sender, EventArgs e)
        {
            // Get modified data from the textboxes
            int userId = int.Parse(textBoxID.Text);
            UserRole userRole = checkBoxEnableAdmin.Checked ? UserRole.Admin : UserRole.User;
            string firstname = textBoxUserFirstName.Text;
            string lastname = textBoxUserLastName.Text;
            string emailAddress = textBoxUserEmailAdd.Text;
            string jobTitle = textBoxUserJobTitle.Text;

            // create new user object from the modified data
            UserCredentialsDto modifiedUser = new UserCredentialsDto
            {
                UserId = userId,
                UserRole = userRole,
                Firstname = firstname,
                Lastname = lastname,
                EmailAddress = emailAddress,
                JobTitle = jobTitle
            };

            // Save the modified user to the database using the presenter
            _presenter.EditUser(modifiedUser);

            // Return to Manage Users page
            panelUserDetails.Visible = false;
            panelManageUsers.Visible = true;
            panelHome.Visible = false;
            panelDocumentButton.Visible = false;

            // Load the user
            await _presenter.LoadUsers();

            // Clear the original values dictionary
            originalValues.Clear();

            // Reset the original checkbox state
            originalCheckBoxState = checkBoxEnableAdmin.Checked;

            // Disable the Save button after saving
            buttonUsersDetailSave.Enabled = false;
        }

        private void buttonCloseUser_Click(object sender, EventArgs e)
        {
            // close panelUserDetails
            panelUserDetails.Visible = false;
            panelManageUsers.Visible = true;
        }

        // event when user fields was changed
        private void TextBox_TextChanged(object? sender, EventArgs e)
        {
            Control textBox = (Control)sender;
            string? originalValue;

            if (originalValues.TryGetValue(textBox, out originalValue))
            {
                string currentValue = textBox.Text;

                // Check if the text has changed
                if (originalValue != currentValue)
                {
                    // Enable the Save button when changes are detected
                    buttonUsersDetailSave.Enabled = true;
                    buttonUsersDetailSave.BackColor = ColorTranslator.FromHtml("#05982E");
                }
                else
                {
                    // Disable the Save button when there are no changes
                    buttonUsersDetailSave.Enabled = false;
                    buttonUsersDetailSave.BackColor = SystemColors.Control;
                }
            }
        }

        private bool originalCheckBoxState;

        private void CheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            // Enable the Save button when the checkbox state changes
            if (originalCheckBoxState != checkBoxEnableAdmin.Checked)
            {
                buttonUsersDetailSave.Enabled = true;
                buttonUsersDetailSave.BackColor = ColorTranslator.FromHtml("#05982E");
            }
            else
            {
                // Disable the Save button when the checkbox state is the same as the original
                buttonUsersDetailSave.Enabled = false;
                buttonUsersDetailSave.BackColor = SystemColors.Control;
            }
        }

        private void checkBoxEnableAdmin_CheckedChanged(object sender, EventArgs e)
        {

        }

        // event when user delete button was clicked
        private async void dataGridViewManageUsers_DeleteButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewManageUsers.Columns["Delete"].Index)
            {
                UserCredentialsDto selectedUser = (UserCredentialsDto)dataGridViewManageUsers.Rows[e.RowIndex].DataBoundItem;
                // Show the delete confirmation modal directly in the main form.
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected user?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    var currentUsersSearchQueryWhenItemDeleted = GetSearchUserQuery();
                    var currentUsersFilterCategoryWhenItemDeleted = GetFilterUsersCategory();

                    await _presenter.DeleteUser(selectedUser);
                    await _presenter.LoadUsersByFilter(currentUsersSearchQueryWhenItemDeleted, currentUsersFilterCategoryWhenItemDeleted);
                }
                else
                {
                    // User clicked "No" or closed the dialog, cancel the deletion
                }
            }
        }

        #endregion

        #region Document Filter and Pagination Functionalities

        private void textBoxSearchBar_TextChanged(object? sender, EventArgs e)
        {
            // When the text changes, stop and restart the timer
            timerSearchBar.Stop();
            timerSearchBar.Start();

        }

        private void comboBoxCategoryDropdown_SelectedIndexChanged(object? sender, EventArgs e) => ApplyFilters(); // Apply the filters when the selected index changes
        private void ApplyFilters() => _presenter.ApplyFilters(); // Call presenter's ApplyFilters method

        private void pictureBox3_Click(object? sender, EventArgs e) => _presenter.NextPage(); // Call presenter's NextPage method

        private void iconBack_Click(object? sender, EventArgs e) => _presenter.PreviousPage(); // Call presenter's PreviousPage method

        // Implement the IMainDocumentView interface methods
        public string GetSearchQuery() => textBoxSearchBar.Text.Trim() ?? string.Empty; // Method for search bar query

        public string GetFilterCategory() => comboBoxCategoryDropdown.SelectedItem?.ToString() ?? string.Empty; // Method for combo box query

        // Method to update the page label for document pagination
        public void UpdatePageLabel(int currentPage, int totalPages)
        {
            // Add condition if totalPages is 0
            if (totalPages == 0)
            {
                labelDocumentPagination.Text = $"Page {currentPage} of {totalPages + 1}";
            }

            else if (totalPages < currentPage && totalPages > 0)
            {
                labelDocumentPagination.Text = $"Page {currentPage - 1} of {totalPages}";
            }

            else
            {
                labelDocumentPagination.Text = $"Page {currentPage} of {totalPages}";
            }

            // Disable back icon if page == 1
            iconBack.Enabled = currentPage <= 1 || totalPages <= 1 ? false : true;

            // Disable next ixon if page == totalpages
            iconNext.Enabled = currentPage == totalPages || totalPages <= 1 ? false : true;
        }

        #endregion

        #region Users Filter and Pagination Functionalities
        private void textBoxUsersSearchBox_TextChanged(object? sender, EventArgs e)
        {
            // When the text changes, stop and restart the timer
            timerUserSearchBar.Stop();
            timerUserSearchBar.Start();
        }

        private void comboBox_JobCategory_SelectedIndexChanged(object? sender, EventArgs e) => ApplyUsersPageFilters(); // Apply  users filter when the selected index changes

        private void ApplyUsersPageFilters() => _presenter.ApplyUsersPageFilters(); // Call presenter's ApplyUsersPageFilters method 

        private void pictureBoxUsersNextIcon_Click(object? sender, EventArgs e) => _presenter.NextUsersPage(); // Call presenter's NextUsersPage method

        private void pictureBoxUsersBackIcon_Click(object? sender, EventArgs e) => _presenter.BackUsersPage(); // Call presenter's BackUsersPage method

        public string GetSearchUserQuery() => textBoxUsersSearchBox.Text.Trim() ?? string.Empty; // Method for search bar query

        public string GetFilterUsersCategory() => comboBox_JobCategory.SelectedItem?.ToString() ?? string.Empty; // Method for combo box query


        public void UpdateUsersPageLabel(int currentPageUsers, int UsersTotalPages)
        {
            // add condition where if UsersTotalPages is 0
            if (UsersTotalPages == 0)
            {
                labelUsersPagination.Text = $"Page {currentPageUsers} of {UsersTotalPages + 1}";
            }

            else if (UsersTotalPages < currentPageUsers && UsersTotalPages > 0)
            {
                labelUsersPagination.Text = $"Page {currentPageUsers - 1} of {UsersTotalPages}";
            }

            else
            {
                labelUsersPagination.Text = $"Page {currentPageUsers} of {UsersTotalPages}";
            }


            // Disable back icon if page == 1
            pictureBoxUsersBackIcon.Enabled = currentPageUsers <= 1 || UsersTotalPages <= 1 ? false : true;

            // Disable next icon if page == totalpages
            pictureBoxUsersNextIcon.Enabled = currentPageUsers == UsersTotalPages || UsersTotalPages <= 1 ? false : true;


        }

        #endregion

        // Implement the SetUsernameLabel method from the iDocument interface
        public void SetUsernameLabel(string username)
        {
            labelHomePageUserLogin.Text = username;
            labelHello.Text = $"Hello, {username}!";
        }

        private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to download all documents?", "Download Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                await _presenter.DownloadAllDocuments(); // Call presenter's DownloadAllDocuments method
            }
        }
        private void buttonSignOut_Click(object sender, EventArgs e) => Application.Exit();


        private void timerSearchBar_Tick(object? sender, EventArgs e) => ApplyFilters(); // Apply the documents filter

        private void timerUserSearchBar_Tick(object sender, EventArgs e) => ApplyUsersPageFilters(); // Apply the users filter

    }
}
