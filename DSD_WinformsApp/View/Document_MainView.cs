using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data;
using DSD_WinformsApp.Infrastructure.Data.Services;
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
    public partial class Document_MainView : Form, IDocumentView
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DocumentPresenter _presenter;
        private string selectedFilePath = "";
        private bool isNewFileUploaded = false;

        public Document_MainView(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _presenter = new DocumentPresenter( this, _unitOfWork.Documents);


        }

        public void BindDataMainView(List<DocumentDto> documents)
        {
            dataGridView1.DataSource = documents;
        }


        private async void DocumentView_Load_1(object sender, EventArgs e)
        {
            // Loads all documents list from data source.
            await _presenter.LoadDocuments();

            // Define the column width from documentmodel
            dataGridView1.Columns["Id"].Width = 60;
            dataGridView1.Columns["Filename"].Width = 300;
            dataGridView1.Columns["Category"].Width = 200;
            dataGridView1.Columns["Status"].Width = 160;
            dataGridView1.Columns["CreatedDate"].Width = 200;
            dataGridView1.Columns["CreatedBy"].Visible = false;
            dataGridView1.Columns["ModifiedBy"].Visible = false;
            dataGridView1.Columns["ModifiedDate"].Visible = false;
            dataGridView1.Columns["Notes"].Visible = false;
            dataGridView1.Columns["FileData"].Visible = false;


            // Add details button functionality
            DataGridViewButtonColumn detailsColumn = new DataGridViewButtonColumn();
            detailsColumn.Text = "Details";
            detailsColumn.Name = "Details";
            detailsColumn.Width = 100;
            detailsColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(detailsColumn);

            // Add delete button functionality
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Text = "Delete";
            deleteColumn.Name = "Delete";
            deleteColumn.Width = 100;
            deleteColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteColumn);


            // Wire up the CellClick event handler
            dataGridView1.CellClick += dataGridView1_DetailsButton_CellClick;
            dataGridView1.CellClick += dataGridView1_DeleteButton_CellClick;

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
        private void dataGridView1_DeleteButton_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                DocumentDto selectedDocument = (DocumentDto)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                // Show the delete confirmation modal directly in the main form.
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected document?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // User clicked "Yes," proceed with the deletion
                    ConfirmDeleteDocument(selectedDocument);
                }
                else
                {
                    // User clicked "No" or closed the dialog, cancel the deletion
                    // Add any additional logic if needed.
                }
            }
        }

        private void ShowDocumentDetailsModal(DocumentDto selectedDocument)
        {
            // Create a new form to display the document details (modal form).
            DetailsFormView detailsForm = new DetailsFormView();

            detailsForm.Text = "Document Details";
            detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            detailsForm.StartPosition = FormStartPosition.CenterParent;

            // Create the buttons and add them to the detailsForm
            Button button1 = new Button();
            button1.Text = "Doc. Details";
            button1.Location = new Point(20, 35); // Adjust the coordinates as needed.
            button1.Height = 40;
            button1.Width = 120;
            detailsForm.Controls.Add(button1);

            Button button2 = new Button();
            button2.Text = "File Details";
            button2.Location = new Point(button1.Right + 10, 35); // Adjust the coordinates as needed.
            button2.Height = button1.Height;
            button2.Width = button1.Width;
            detailsForm.Controls.Add(button2);

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
            Button uploadFileButton = new Button();
            uploadFileButton.Text = "Upload File";
            uploadFileButton.Location = new Point(filenameTextBox.Right + 10, filenameTextBox.Top); // Right next to the Filename TextBox
            uploadFileButton.Height = filenameTextBox.Height; // Match the height of the TextBox
            uploadFileButton.Width = 120; // Adjust the width as needed
            uploadFileButton.Enabled = false; // Disable the button initially
            uploadFileButton.Click += (sender, e) => UploadFileButton_Click(sender, e, filenameTextBox);
            groupBox.Controls.Add(uploadFileButton);

            // Create the Category ComboBox
            ComboBox categoryComboBox = new ComboBox();
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Make it a drop-down list
            categoryComboBox.Items.Add("Category 1"); // Add your category options here
            categoryComboBox.Items.Add("Category 2");
            categoryComboBox.Items.Add("Category 3");
            categoryComboBox.Text = selectedDocument.Category; // Set the initial selected item
            categoryComboBox.Enabled = false; // Disable the ComboBox initially, enable it when editing
            int categoryComboBoxWidth = textBoxWidth; // Adjust the width as needed
            AddRow(groupBox, "Category:", categoryComboBox, categoryComboBoxWidth);

            // Create the Status ComboBox
            ComboBox statusComboBox = new ComboBox();
            statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Make it a drop-down list
            statusComboBox.Items.Add("Open"); // Add your status options here
            statusComboBox.Items.Add("In Progress");
            statusComboBox.Items.Add("Closed");
            statusComboBox.Text = selectedDocument.Status; // Set the initial selected item
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

            int notesTextBoxWidth = textBoxWidth ; // Adjust the width as needed
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
            int groupBox2Width = detailsForm.ClientSize.Width - 50; // 40 pixels less than the client width of DetailsFormView
            int groupBox2Height = detailsForm.ClientSize.Height - groupBox2.Top - 40; // 40 pixels buffer at the bottom
            groupBox2.Size = new Size(groupBox2Width, groupBox2Height);

            // Get the data for the second DataGridView (DataGridView2)
            IList<object> dataForDataGridView2 = dataGridView1.Rows.Cast<DataGridViewRow>()
                .Select(row => new
                {
                    Id = row.Cells["Id"].Value,
                    Filename = row.Cells["Filename"].Value
                }).ToList<object>();

            // Bind the data to the second DataGridView (DataGridView2)
            dataGridView2.DataSource = new BindingList<object>(dataForDataGridView2);

            // Set the width of each column in dataGridView2 manually
            dataGridView2.Columns["Id"].Width = 50;
            dataGridView2.Columns["Filename"].Width = 400; 

            // Add download button functionality
            DataGridViewButtonColumn downloadColumn = new DataGridViewButtonColumn();
            downloadColumn.Text = "Download";
            downloadColumn.Name = "";
            downloadColumn.Width = 100;
            downloadColumn.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(downloadColumn);

            // Add delete button functionality
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Text = "Delete";
            deleteColumn.Name = "";
            deleteColumn.Width = 100;
            deleteColumn.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(deleteColumn);



            // Create the Edit button
            Button editButton = new Button();
            editButton.Text = "Edit";
            editButton.Name = "editButton";
            editButton.Location = new Point(groupBox.Right - editButton.Width, groupBox.Bottom + 10);
            editButton.Height = 40;
            editButton.Width = 80;
            editButton.Click += EditButton_Click;
            detailsForm.Controls.Add(editButton);

            // Create the Close button
            Button closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Name = "closeButton";
            closeButton.Location = new Point(editButton.Left - 10 - closeButton.Width, groupBox.Bottom + 10);
            closeButton.Height = editButton.Height;
            closeButton.Width = editButton.Width;
            closeButton.Click += CloseButton_Click;
            detailsForm.Controls.Add(closeButton);

            // Create the Save button
            Button saveButton = new Button(); // Define the saveButton as a local variable
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
                groupBox.Visible = true; // Show the GroupBox when Button 1 is clicked.
            };

            // Handle the Click event of Button 2
            button2.Click += (sender, e) =>
            {
                groupBox.Visible = false; // hide the GroupBox when Button 1 is clicked.
                groupBox2.Visible = true; // Show the GroupBox containing DataGridView2 when Button 2 is clicked.
    
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

        }

        private async void ConfirmDeleteDocument(DocumentDto selectedDocument)
        {
             await _presenter.DeleteDocument(selectedDocument);
            // Load the documents again to update the view
            await _presenter.LoadDocuments();
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

                // Check if the selected file is different from the current file
                if (filePath != filenameTextBox.Tag as string)
                {

                    // Update the TextBox with the selected file name
                    filenameTextBox.Text = Path.GetFileName(filePath);

                    // Set the flag to indicate that a new file has been uploaded
                    isNewFileUploaded = true;
                }

                // Get the filename from the selected file path
                string selectedFileName = Path.GetFileNameWithoutExtension(filePath);

                // Check if a file already exists with the same name
                if (filenameTextBox.Text == selectedFileName)
                {
                    DialogResult result = MessageBox.Show("A file with the same name already exists. Do you want to replace it?", "File Replacement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return; // User chose not to replace the file, so exit the method.
                    }
                }

                // Update the TextBox with the selected file name
                filenameTextBox.Text = Path.GetFileNameWithoutExtension(filePath);
                // Set the Tag property of filenameTextBox to store the selected file path
                filenameTextBox.Tag = filePath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (AddForm newForm = new AddForm(_unitOfWork, _presenter))
            {
                newForm.StartPosition = FormStartPosition.CenterParent;
                newForm.ShowDialog();
            }
        }

    }
}
