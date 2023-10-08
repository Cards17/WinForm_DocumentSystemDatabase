using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data;
using DSD_WinformsApp.Infrastructure.Data.Services;
using DSD_WinformsApp.Model;
using DSD_WinformsApp.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            ToggleManageUsersButtonVisibility(isVisible); // Manage Users button visibility

            // Documents filter events 
            textBoxSearchBar.TextChanged += textBoxSearchBar_TextChanged;
            comboBoxCategory.TextChanged += comboBoxCategoryDropdown_SelectedIndexChanged;

            // Users filter events
            textBoxUsersSearchBox.TextChanged += textBoxUsersSearchBox_TextChanged;
            comboBox_JobCategory.TextChanged += comboBox_JobCategory_SelectedIndexChanged;
        }

        private async void DocumentView_Load_1(object sender, EventArgs e)
        {
            // Load the documents from the database using the presenter
            await _presenter.LoadDocumentsByFilter(currentSearchQuery, currentFilterCategory);

            // Load the users from the database using the presenter
            await _presenter.LoadUsersByFilter(currentSearchUserQuery, currentJobFilter);

            // DONT TRY TO REMOVE THIS COMMENTED CODE
            //buttonUsersDetailSave = new CustomButton(ColorTranslator.FromHtml("#05982E"), SystemColors.Control);
            //buttonCloseUser = new CustomButton(ColorTranslator.FromHtml("#DA0B0B"), SystemColors.Control);
            //buttonEditUser = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);


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
            dataGridView1.Columns["Id"].Width = 55;
            dataGridView1.Columns["Filename"].Width = 300;
            dataGridView1.Columns["Category"].Width = 170;
            dataGridView1.Columns["Status"].Width = 160;
            dataGridView1.Columns["CreatedDate"].Width = 155;
            dataGridView1.Columns["CreatedBy"].Visible = false;
            dataGridView1.Columns["ModifiedBy"].Visible = false;
            dataGridView1.Columns["ModifiedDate"].Visible = false;
            dataGridView1.Columns["Notes"].Visible = false;
            dataGridView1.Columns["FileData"].Visible = false;

            // Display name for table columns
            dataGridView1.Columns["Id"].HeaderText = "Id";
            dataGridView1.Columns["Status"].HeaderText = "File Status";
            dataGridView1.Columns["Filename"].HeaderText = "Filename";
            dataGridView1.Columns["Category"].HeaderText = "File Category";
            dataGridView1.Columns["CreatedDate"].HeaderText = "Created Date";


            // Add details button functionality
            DataGridViewButtonColumn detailsColumn = new DataGridViewButtonColumn();
            detailsColumn.Text = "Details";
            detailsColumn.Name = "Details";
            detailsColumn.Width = 93;
            detailsColumn.UseColumnTextForButtonValue = true;
            detailsColumn.HeaderText = string.Empty;
            dataGridView1.Columns.Add(detailsColumn);


            // add download button here and then when the button was clicked i need to download the file into downloads folder
            DataGridViewButtonColumn downloadColumn = new DataGridViewButtonColumn();
            downloadColumn.Text = "Download";
            downloadColumn.Name = "Download";
            downloadColumn.Width = 93;
            downloadColumn.UseColumnTextForButtonValue = true;
            downloadColumn.HeaderText = string.Empty;
            dataGridView1.Columns.Add(downloadColumn);

            // Add delete button functionality
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Text = "Delete";
            deleteColumn.Name = "Delete";
            deleteColumn.Width = 93;
            deleteColumn.UseColumnTextForButtonValue = true;
            deleteColumn.HeaderText = string.Empty;
            dataGridView1.Columns.Add(deleteColumn);

            // Wire up the CellClick event handler
            dataGridView1.CellClick += dataGridView1_DetailsButton_CellClick;
            dataGridView1.CellClick += dataGridView1_DeleteButton_CellClick;
            dataGridView1.CellClick += dataGridView1_DownloadButton_CellClick;

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



        public void BindDataMainView(List<DocumentDto> documents)
        {
            dataGridView1.DataSource = documents;
        }

        public void BindDataManageUsers(List<UserCredentialsDto> users)
        {
            dataGridViewManageUsers.DataSource = users;
        }

        #region Document Page Methods

        private void buttonDocument_Click(object sender, EventArgs e)
        {
            panelManageUsers.Visible = false; // Hide the Manage Users panel
            panelHome.Visible = false; // Hide the Home panel
            panelUserDetails.Visible = false; // Hide the User Details panel
            panelDocumentButton.Visible = true;
        }

        private void dataGridView1_DownloadButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            //implement download file functionality here
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Download"].Index)
            {
                DocumentDto selectedDocument = (DocumentDto)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                // Show the delete confirmation modal directly in the main form.
                DialogResult result = MessageBox.Show("Are you sure you want to download the selected document?", "Download Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // User clicked "Yes," proceed with the deletion
                    ConfirmDownloadDocument(selectedDocument);
                }
                else
                {
                    // User clicked "No" or closed the dialog, cancel the deletion
                    // Add any additional logic if needed.
                }
            }
        }

        private void ConfirmDownloadDocument(DocumentDto selectedDocument)
        {
            try
            {
                // Get the file data (byte array) from the selectedDocument
                byte[] fileData = selectedDocument.FileData;

                // Distination path in the user's "Downloads" folder
                string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string destinationFilePath = Path.Combine(downloadsPath, "Downloads", selectedDocument.Filename);

                // Write the file data to the destination file
                File.WriteAllBytes(destinationFilePath, fileData);

                // Show a message to indicate the download completion
                MessageBox.Show("File downloaded successfully!", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading the file: {ex.Message}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Event method when details button was clicked
        private void dataGridView1_DetailsButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Details"].Index)
            {
                DocumentDto selectedDocument = (DocumentDto)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                ShowDocumentDetailsModal(selectedDocument);
            }
        }

        //Event method when details button was clicked
        private async void dataGridView1_DeleteButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                DocumentDto selectedDocument = (DocumentDto)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                // Show the delete confirmation modal directly in the main form.
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected document?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    // Delete the document and its backups from the database using the presenter
                    await _presenter.DeleteDocumentWithBackups(selectedDocument);

                    // Load the documents again to update the view
                    await _presenter.LoadDocuments();

                }
                else
                {
                    // User clicked "No" or closed the dialog, cancel the deletion
                    // Add any additional logic if needed.
                }
            }
        }

        private async void ShowDocumentDetailsModal(DocumentDto selectedDocument)
        {
            // Create a new form to display the document details (modal form).
            DetailsFormView detailsForm = new DetailsFormView();
            detailsForm.Text = "Document Details";
            detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            detailsForm.StartPosition = FormStartPosition.CenterParent;

            #region Document Details
            // Create the GroupBox
            GroupBox groupBox = new GroupBox();
            groupBox.Text = "Document Details";
            groupBox.AutoSize = false; // Set AutoSize to false.
            groupBox.Width = detailsForm.ClientSize.Width; // Set the width to match detailsForm's client width.
            groupBox.Height = detailsForm.ClientSize.Height; // Set the height to match detailsForm's client height.
            groupBox.Location = new Point(20, 80); // Adjust the coordinates as needed.
            groupBox.Visible = true;

            // Add the GroupBox to the detailsForm
            detailsForm.Controls.Add(groupBox);

            int textBoxWidth = 450; // You can adjust the default width for TextBox controls

            // Create a TextBox for "Filename"
            TextBox filenameTextBox = new TextBox();
            filenameTextBox.Text = selectedDocument.Filename;
            filenameTextBox.ReadOnly = true;
            int filenameTextBoxWidth = textBoxWidth - 130; // Adjust the width as needed
            AddRow(groupBox, "Filename:", filenameTextBox, filenameTextBoxWidth);

            // Create the "Upload File" button and pass the filenameTextBox as a parameter
            CustomButton uploadFileButton = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            uploadFileButton.Text = "Upload File";
            uploadFileButton.Location = new Point(filenameTextBox.Right + 10, filenameTextBox.Top - 5); // Right next to the Filename TextBox
            uploadFileButton.Height = filenameTextBox.Height + 10; // Match the height of the TextBox
            uploadFileButton.Width = 120; // Adjust the width as needed
            uploadFileButton.Enabled = false; // Disable the button initially
            uploadFileButton.Click += (sender, e) => UploadFileButton_Click(sender, e, filenameTextBox);
            groupBox.Controls.Add(uploadFileButton);

            // Create the Category ComboBox
            ComboBox categoryComboBox = new ComboBox();
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Make it a drop-down list
            categoryComboBox.Items.Add("Board Resolutions");
            categoryComboBox.Items.Add("Canteen Policies");
            categoryComboBox.Items.Add("COOP Policies");
            categoryComboBox.Items.Add("COOP Article & By Laws");
            categoryComboBox.Items.Add("Minutes of the Meeting");
            categoryComboBox.Items.Add("Regulatory Requirements");
            categoryComboBox.Text = selectedDocument.Category.ToString(); // Set the initial selected item
            categoryComboBox.Enabled = false; // Disable the ComboBox initially, enable it when editing
            int categoryComboBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Category:", categoryComboBox, categoryComboBoxWidth);

            // Create the Status ComboBox
            ComboBox statusComboBox = new ComboBox();
            statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Make it a drop-down list;
            statusComboBox.Items.Add("New");
            statusComboBox.Items.Add("Revised");
            statusComboBox.Items.Add("Obsolete");
            statusComboBox.SelectedItem = selectedDocument.Status.ToString(); // Set the initial selected item
            statusComboBox.Enabled = false; // Disable the ComboBox initially, enable it when editing
            int statusComboBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Status:", statusComboBox, statusComboBoxWidth);

            // Create a TextBox for the "Created Date" property and set its initial value
            TextBox createdDateTextBox = new TextBox();
            createdDateTextBox.Text = selectedDocument.CreatedDate.ToString("yyyy-MM-dd");
            createdDateTextBox.ReadOnly = true;
            int createdDateTextBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Created Date:", createdDateTextBox, createdDateTextBoxWidth);

            // Create a TextBox for the "Created By" property and set its initial value
            TextBox createdByTextBox = new TextBox();
            createdByTextBox.Text = selectedDocument.CreatedBy;
            createdByTextBox.ReadOnly = true;
            int createdByTextBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Created By:", createdByTextBox, createdByTextBoxWidth);

            // Create a TextBox for the "Modified By" property and set its initial value
            TextBox modifiedByTextBox = new TextBox();
            modifiedByTextBox.Text = selectedDocument.ModifiedBy;
            modifiedByTextBox.ReadOnly = true;
            int modifiedByTextBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Modified By:", modifiedByTextBox, modifiedByTextBoxWidth);

            // Create a TextBox for the "Modified Date" property and set its initial value
            TextBox modifiedDateTextBox = new TextBox();
            modifiedDateTextBox.Text = selectedDocument.ModifiedDate.ToString("yyyy-MM-dd");
            modifiedDateTextBox.ReadOnly = true;
            int modifiedDateTextBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Modified Date:", modifiedDateTextBox, modifiedDateTextBoxWidth);

            // Create a multiline TextBox for the "Notes" property
            TextBox notesTextBox = new TextBox();
            notesTextBox.Text = selectedDocument.Notes;
            notesTextBox.ReadOnly = true;
            notesTextBox.Multiline = true;
            notesTextBox.MaxLength = 200; // Set the maximum length to 150 characters
            notesTextBox.Height = 150; // Set the fixed height to 100 pixels (adjust the value as needed)

            int notesTextBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Notes:", notesTextBox, notesTextBoxWidth);

            // Function to add a row (label + control) to the GroupBox
            void AddRow(GroupBox parent, string labelText, Control control, int controlWidth)
            {
                Label label = new Label();
                label.Text = labelText;
                label.AutoSize = true;

                int labelTop = parent.Controls.Count * 20 + 50;
                label.Location = new Point(50, labelTop);

                int controlLeft = label.Right + 80;
                control.Location = new Point(controlLeft, labelTop);
                control.Width = controlWidth;

                parent.Controls.Add(label);
                parent.Controls.Add(control);
            }

            // Adjust the size of the GroupBox to fit its contents
            int groupBoxWidth = 750;
            int groupBoxHeight = groupBox.Controls.Count * 30 + 30; // Add some buffer (e.g., 40 pixels) to avoid cutting off any controls.
            groupBox.Width = groupBoxWidth;
            groupBox.Height = groupBoxHeight;

            #endregion

            #region File Details

            // Create the buttons and add them to the detailsForm
            CustomButton button1 = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            button1.Text = "File Details";
            button1.Location = new Point(20, 35); // Adjust the coordinates as needed.
            button1.Height = 40;
            button1.Width = 120;
            detailsForm.Controls.Add(button1);

            CustomButton button2 = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            button2.Text = "File History";
            button2.Location = new Point(button1.Right + 10, 35);
            button2.Height = button1.Height;
            button2.Width = button1.Width;
            detailsForm.Controls.Add(button2);

            // Create the GroupBox containing the second DataGridView (DataGridView2)
            GroupBox groupBox2 = new GroupBox();
            groupBox2.Text = "File Details";
            groupBox2.AutoSize = true;
            groupBox2.Location = new Point(20, button2.Bottom + 20); // Adjust the position as needed
            groupBox2.Visible = false; // Set the initial visibility to false
            detailsForm.Controls.Add(groupBox2);

            DataGridView dataGridView2 = new DataGridView();
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;
            groupBox2.Controls.Add(dataGridView2);


            // Adjust the size of the DetailsFormView to fit the contents including groupBox2
            int groupBox2Width = detailsForm.ClientSize.Width - 50;
            int groupBox2Height = detailsForm.ClientSize.Height - groupBox2.Top - 40;
            groupBox2.Size = new Size(groupBox2Width, groupBox2Height);

            // Binding related backup file data to dataGridView2 based on the selected document's ID
            var relatedBackupFiles = await _presenter.GetRelatedBackupFiles(selectedDocument.Id);
            dataGridView2.DataSource = relatedBackupFiles;


            // Display Columns in datagridview2
            dataGridView2.Columns["BackupId"].Width = 55;
            dataGridView2.Columns["Filename"].Width = 300;
            dataGridView2.Columns["BackupDate"].Width = 100;
            dataGridView2.Columns["Version"].Width = 85;

            dataGridView2.Columns["BackupId"].HeaderText = "Id";
            dataGridView2.Columns["Filename"].HeaderText = "Filename";
            dataGridView2.Columns["BackupDate"].HeaderText = "Date";

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
                            // Source path of the backup file
                            string sourceFilePath = selectedBackupFile.BackupFilePath;

                            // Destination path in the user's "Downloads" folder
                            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                            string destinationFilePath = Path.Combine(downloadsPath, "Downloads", selectedBackupFile.Filename);

                            // Copy the file from source to destination
                            File.Copy(sourceFilePath, destinationFilePath, true);

                            // Show a message to indicate the download completion
                            MessageBox.Show("File downloaded successfully!", "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error downloading the file: {ex.Message}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        DialogResult result = MessageBox.Show("Are you sure you want to delete this file?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                // Delete the file
                                await _presenter.DeleteBackUpFile(selectedBackupFile);

                                // Refresh the DataGridView
                                relatedBackupFiles.Remove(selectedBackupFile);
                                dataGridView2.DataSource = null;
                                dataGridView2.DataSource = relatedBackupFiles;

                                // Display Columns in datagridview2
                                dataGridView2.Columns["BackupId"].DisplayIndex = 0;
                                dataGridView2.Columns["Filename"].DisplayIndex = 1;
                                dataGridView2.Columns["BackupDate"].DisplayIndex = 2;

                                dataGridView2.Columns["BackupId"].HeaderText = "Doc. ID";
                                dataGridView2.Columns["Filename"].HeaderText = "File Name";
                                dataGridView2.Columns["BackupDate"].HeaderText = "Upload Date";

                                dataGridView2.Columns["BackupId"].Width = 50;
                                dataGridView2.Columns["Filename"].Width = 320;
                                dataGridView2.Columns["BackupDate"].Width = 170;

                                dataGridView2.Columns["OriginalFilePath"].Visible = false;
                                dataGridView2.Columns["BackupFilePath"].Visible = false;
                                dataGridView2.Columns["Id"].Visible = false;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error deleting the file: {ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string filename = filenameTextBox.Text;
                string category = categoryComboBox.Text;
                string status = statusComboBox.Text;
                DateTime createdDate = DateTime.Parse(createdDateTextBox.Text);
                string createdBy = createdByTextBox.Text;
                string modifiedBy = modifiedByTextBox.Text;
                DateTime modifiedDate = DateTime.Parse(modifiedDateTextBox.Text);
                string notes = notesTextBox.Text;

                // Check if the file name has been changed
                if (filename != Path.GetFileName(filePath))
                {
                    // Update the filenameTextBox with the new file name
                    filenameTextBox.Text = Path.GetFileNameWithoutExtension(filePath);
                }


                // Create a new DocumentDto with the modified data
                DocumentDto modifiedDocument = new DocumentDto
                {
                    Id = selectedDocument.Id,
                    Filename = filename,
                    Category = category,
                    Status = status,
                    CreatedDate = createdDate,
                    CreatedBy = createdBy,
                    ModifiedBy = modifiedBy,
                    ModifiedDate = modifiedDate,
                    Notes = notes
                };

                // Save the modified document to the database using the presenter
                _presenter.EditDocument(modifiedDocument, fileDataBytes);

                // Load the documents again to update the view
                await _presenter.LoadDocuments();

                // Show a confirmation dialog
                var result = MessageBox.Show("Document has been successfully saved. Do you want to proceed to MainView?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // Check if the user clicked "Yes"
                if (result == DialogResult.Yes)
                {
                    // Close the DetailsForm (current form) to go back to MainView
                    detailsForm.Close();
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


                // Reset the flag for the next upload
                isNewFileUploaded = false;

                // Disable the Save button after saving
                saveButton.Enabled = false;
                // Re-enable the Edit button
                editButton.Enabled = true;
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

                // Enable the Upload File button only if there's a file uploaded
                uploadFileButton.Enabled = true;

                // Disable the Edit button after enabling editing
                editButton.Enabled = false;
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
                // Enable editing of the controls inside the GroupBox except for Created Date and Modified Date.
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

        private void UploadFileButton_Click(object? sender, EventArgs e, TextBox filenameTextBox)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                // Get the selected file path
                string filePath = openFileDialog.FileName;
                string filePath_Filename = Path.GetFileName(filePath);

                // Check if the selected file is different from the current file
                if (filePath_Filename != filenameTextBox.Tag as string)
                {
                    // Update the TextBox with the selected file name
                    filenameTextBox.Text = filePath_Filename;

                    // Set the flag to indicate that a new file has been uploaded
                    isNewFileUploaded = true;
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

        public void ShowDocumentView()
        {
            // Show the DocumentView form
            this.ShowDialog();

        }

        // Handler for the FormClosing event
        private void DocumentViewForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Make sure the form is closing explicitly (e.g., by clicking the close icon)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Close the entire application
                Application.Exit();
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
        public void ToggleManageUsersButtonVisibility(bool isVisible)
        {
            buttonManageUsers.Visible = isVisible;
        }
        private async void buttonManageUsers_Click(object sender, EventArgs e)
        {

            await _presenter.LoadUsers();

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
            dataGridViewManageUsers.Columns["CreatedDate"].DisplayIndex = 5;

            // Set the column widths
            dataGridViewManageUsers.Columns["UserId"].Width = 60;
            dataGridViewManageUsers.Columns["Firstname"].Width = 155;
            dataGridViewManageUsers.Columns["Lastname"].Width = 155;
            dataGridViewManageUsers.Columns["EmailAddress"].Width = 300;
            dataGridViewManageUsers.Columns["JobTitle"].Width = 270;

            dataGridViewManageUsers.Columns["UserId"].Visible = true;
            dataGridViewManageUsers.Columns["Firstname"].Visible = true;
            dataGridViewManageUsers.Columns["Lastname"].Visible = true;
            dataGridViewManageUsers.Columns["EmailAddress"].Visible = true;
            dataGridViewManageUsers.Columns["JobTitle"].Visible = true;
            dataGridViewManageUsers.Columns["CreatedDate"].Visible = false;
            dataGridViewManageUsers.Columns["Password"].Visible = false;
            dataGridViewManageUsers.Columns["ImageData"].Visible = false;
            dataGridViewManageUsers.Columns["UserRole"].Visible = false;

            // Display name for headertext
            dataGridViewManageUsers.Columns["UserId"].HeaderText = "ID";
            dataGridViewManageUsers.Columns["Firstname"].HeaderText = "First Name";
            dataGridViewManageUsers.Columns["Lastname"].HeaderText = "Last Name";
            dataGridViewManageUsers.Columns["EmailAddress"].HeaderText = "Email Address";
            dataGridViewManageUsers.Columns["JobTitle"].HeaderText = "Job Title";
            dataGridViewManageUsers.Columns["CreatedDate"].HeaderText = "Created Date";
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

                // Default button colors
                buttonUsersDetailSave.BackColor = ColorTranslator.FromHtml("#05982E");
                buttonCloseUser.BackColor = ColorTranslator.FromHtml("#DA0B0B");
                buttonEditUser.BackColor = ColorTranslator.FromHtml("#A5D7E8");

                // button states
                buttonUsersDetailSave.Enabled = false;
                buttonEditUser.Enabled = true;
                buttonCloseUser.Enabled = true;

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
        }

        private Dictionary<Control, string> originalValues = new Dictionary<Control, string>();

        private void buttonEditUser_Click(object sender, EventArgs e)
        {
            buttonEditUser.Enabled = false; // Disable the Edit button
            buttonUsersDetailSave.Enabled = true; // Enable the Save button
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
            originalValues.Add(checkBoxEnableAdmin, checkBoxEnableAdmin.Checked.ToString());
            originalValues.Add(textBoxUserFirstName, textBoxUserFirstName.Text);
            originalValues.Add(textBoxUserLastName, textBoxUserLastName.Text);
            originalValues.Add(textBoxUserEmailAdd, textBoxUserEmailAdd.Text);
            originalValues.Add(textBoxUserJobTitle, textBoxUserJobTitle.Text);
        }


        private async void buttonUsersDetailSave_Click(object sender, EventArgs e)
        {
            // Compare the current values with the original values to check for changes
            bool hasChanges = false;

            foreach (var kvp in originalValues)
            {
                Control control = kvp.Key;
                string originalValue = kvp.Value;
                string currentValue = control.Text;

                if (originalValue != currentValue)
                {
                    // There is a change in this field
                    hasChanges = true;
                    // You can process or save the changes here
                }
            }

            if (hasChanges)
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

            }
            else
            {
                // No changes detected, provide a message or take appropriate action
            }

            

        }

        private void buttonCloseUser_Click(object sender, EventArgs e)
        {
            // close panelUserDetails
            panelUserDetails.Visible = false;
            panelManageUsers.Visible = true;
        }

        private void checkBoxEnableAdmin_CheckedChanged(object sender, EventArgs e)
        {

        }

        // event when delete button was clicked
        private async void dataGridViewManageUsers_DeleteButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewManageUsers.Columns["Delete"].Index)
            {
                UserCredentialsDto selectedUser = (UserCredentialsDto)dataGridViewManageUsers.Rows[e.RowIndex].DataBoundItem;
                // Show the delete confirmation modal directly in the main form.
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected document?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // User clicked "Yes," proceed with the deletion
                    await _presenter.DeleteUser(selectedUser);
                    await _presenter.LoadUsers();
                }
                else
                {
                    // User clicked "No" or closed the dialog, cancel the deletion
                    // Add any additional logic if needed.
                }
            }
        }





        #endregion

        #region Document Filter and Pagination Functionalities

        private void textBoxSearchBar_TextChanged(object? sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void comboBoxCategoryDropdown_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            _presenter.ApplyFilters();
        }

        private void pictureBox3_Click(object? sender, EventArgs e)
        {
            _presenter.NextPage();
        }

        private void iconBack_Click(object? sender, EventArgs e)
        {
            _presenter.PreviousPage();
        }


        // Implement the IMainDocumentView interface methods
        public string GetSearchQuery()
        {
            return textBoxSearchBar.Text.Trim() ?? string.Empty;
        }

        public string GetFilterCategory()
        {
            return comboBoxCategoryDropdown.SelectedItem?.ToString() ?? string.Empty;
        }

        // Method to update the page label for users pagination
        public void UpdatePageLabel(int currentPage, int totalPages)
        {
            // Add condition if totalPages is 0
            if (totalPages == 0)
            {
                labelDocumentPagination.Text = $"Page {currentPage} of {totalPages + 1}";
            }
            else
            {
                labelDocumentPagination.Text = $"Page {currentPage} of {totalPages}";
            }
        }

        #endregion

        #region Users Filter and Pagination Functionalities
        private void textBoxUsersSearchBox_TextChanged(object? sender, EventArgs e)
        {
            ApplyUsersPageFilters();
        }

        private void comboBox_JobCategory_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ApplyUsersPageFilters();
        }

        private void ApplyUsersPageFilters()
        {
            _presenter.ApplyUsersPageFilters();
        }

        private void pictureBoxUsersNextIcon_Click(object? sender, EventArgs e)
        {
            _presenter.NextUsersPage();
        }

        private void pictureBoxUsersBackIcon_Click(object? sender, EventArgs e)
        {
            _presenter.BackUsersPage();
        }

        public string GetSearchUserQuery()
        {
            return textBoxUsersSearchBox.Text.Trim() ?? string.Empty;
        }

        public string GetFilterUsersCategory()
        {
            return comboBox_JobCategory.SelectedItem?.ToString() ?? string.Empty;
        }

        public void UpdateUsersPageLabel(int currentPageUsers, int UsersTotalPages)
        {
            // add condition where if UsersTotalPages is 0
            if (UsersTotalPages == 0)
            {
                labelUsersPagination.Text = $"Page {currentPageUsers} of {UsersTotalPages + 1}";
            }
            else
            {
                labelUsersPagination.Text = $"Page {currentPageUsers} of {UsersTotalPages}";
            }

        }

        #endregion

        // Implement the SetUsernameLabel method from the iDocument interface
        public void SetUsernameLabel(string username)
        {
            // Assuming labelUsername is the name of your label control
            labelHomePageUserLogin.Text = "Hello, " + username;
        }



        private void buttonSignOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
