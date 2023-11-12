using DSD_WinformsApp.Core.DTOs;
using DSD_WinformsApp.Infrastructure.Data;
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
    public partial class AddFormView : Form
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDocumentPresenter _presenter;

        private ErrorProvider errorProvider = null!; // Class-level variable to store the ErrorProvider component

        private string selectedFilePath = null!; // Class-level variable to store the selected file path

        public AddFormView(IUnitOfWork unitOfWork, IDocumentPresenter presenter)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _presenter = presenter;


            MaximizeBox = false; // Remove the maximize box
            MinimizeBox = false; // Remove the minimize box

            errorProvider = new ErrorProvider();  // Initialize the ErrorProvider component

            // Initialize the ComboBox controls
            StatusComboBox();
            CategoryComboBox();
            CreatedByComboBox();

        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            MaximizeBox = false; // Remove the maximize box

            btnSave.Enabled = false; // Disable the Save button initially

            labelFilename.Visible = false; // Hide the label that displays the selected file name

            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink; // remove the blinking icon when error occurs

            // Set the background color of buttons initially.
            btnSave.BackColor = SystemColors.Control;
            buttonUploadDocs.BackColor = ColorTranslator.FromHtml("#A5D7E8");
            btnCancel.BackColor = ColorTranslator.FromHtml("#DA0B0B");

            // Attach SelectedIndexChanged event handlers to ComboBox controls
            cmbCategories.SelectedIndexChanged += Control_SelectedIndexChanged;
            comboBoxCreatedBy.SelectedIndexChanged += Control_SelectedIndexChanged;
            cmbStatus.SelectedIndexChanged += Control_SelectedIndexChanged;

            // Attach TextChanged event handlers to relevant controls
            labelFilename.TextChanged += Control_TextChanged;
            textBoxDocumentVersion.TextChanged += Control_TextChanged;
            txtBoxNotes.TextChanged += Control_TextChanged;
        }

        // Status selection
        private void StatusComboBox()
        {
            cmbStatus.Items.Add("New");
            cmbStatus.Items.Add("Revised");
            cmbStatus.Items.Add("Obsolete");
        }

        // Category selection
        private void CategoryComboBox()
        {
            cmbCategories.Items.Add("Board Resolutions");
            cmbCategories.Items.Add("Canteen Policies");
            cmbCategories.Items.Add("COOP Policies");
            cmbCategories.Items.Add("COOP Article & By Laws");
            cmbCategories.Items.Add("Minutes of the Meeting");
            cmbCategories.Items.Add("Regulatory Requirements");
        }

        // Add method for combo box items from the database
        private async void CreatedByComboBox()
        {
            var users = await _presenter.GetAllRegisteredUsers();
            foreach (var user in users)
            {
                string fullName = user.Firstname + " " + user.Lastname;
                comboBoxCreatedBy.Items.Add(fullName);
            }
        }


        private void btnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                // Validate filePath 
                if (string.IsNullOrEmpty(selectedFilePath))
                {
                    MessageBox.Show("Please select a document before saving.");
                    return;
                }

                byte[] fileDataBytes = File.ReadAllBytes(selectedFilePath);

                var documentDto = new DocumentDto
                {
                    Filename = labelFilename.Text,
                    FilenameExtension = labelDocumentNameWithExtension.Text,
                    DocumentVersion = textBoxDocumentVersion.Text.ToUpper(),
                    Category = cmbCategories.SelectedItem?.ToString() ?? "",
                    Status = cmbStatus.SelectedItem?.ToString() ?? "",
                    Notes = txtBoxNotes.Text,
                    CreatedBy = comboBoxCreatedBy.SelectedItem?.ToString() ?? "",
                    CreatedDate = DateTime.Now.Date,
                    ModifiedDate = DateTime.Now.Date,
                };

                _presenter.SaveDocument(documentDto, fileDataBytes); // Save added document to database

                _presenter.AddNewDocument(documentDto); // Reflect added document in documentview
            }
            catch (Exception)
            {
                MessageBox.Show("An unexpected error occurred while saving the document. Please contact support for assistance.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DialogResult = DialogResult.OK; // Set DialogResult in the finally block
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close the form and return DialogResult.Cancel
            DialogResult = DialogResult.Cancel;
        }

        private async void buttonUploadDocs_Click(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedExtension = Path.GetExtension(openFileDialog.FileName); // Get the selected file extension
                List<string> allowedExtensions = new List<string> { ".docx", ".doc", ".xlsx", ".xls", ".pdf" }; // Allowed file extensions
                if (!allowedExtensions.Contains(selectedExtension))
                {
                    MessageBox.Show("Invalid document type. Please select a Word, PDF, or Excel document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                selectedFilePath = openFileDialog.FileName; // Store the selected file path

                // Display only the file name without the extension in the label and the TextBox
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                labelFilename.Text = fileNameWithoutExtension;

                // Get filename with extension for saving to db
                string fileExtension = Path.GetExtension(openFileDialog.FileName);
                labelDocumentNameWithExtension.Text = fileExtension;


                // Check for duplicate file name in the repository
                bool hasDuplicateFileName = await _presenter.CheckForDuplicateFileName(fileNameWithoutExtension);

                if (hasDuplicateFileName)
                {
                    MessageBox.Show($"{fileNameWithoutExtension} already exists. Please rename the document.", "Duplicate File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            // labelFilename.Enabled = false;
            labelFilename.Visible = true;

        }

        private void Control_TextChanged(object? sender, EventArgs e)
        {
            ValidateForm();
        }

        private void Control_SelectedIndexChanged(object? sender, EventArgs e)
        {
            ValidateForm();
        }

        private void ValidateForm()
        {
            bool isValid = true;
            errorProvider.Clear();


            if (string.IsNullOrWhiteSpace(labelFilename.Text))
            {
                errorProvider.SetError(buttonUploadDocs, "Upload file is required.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(textBoxDocumentVersion.Text))
            {
                errorProvider.SetError(textBoxDocumentVersion, "Document Version is required.");
                isValid = false;
            }

            if (cmbCategories.SelectedItem == null)
            {
                errorProvider.SetError(cmbCategories, "Document Category is required.");
                isValid = false;
            }

            if (cmbStatus.SelectedItem == null)
            {
                errorProvider.SetError(cmbStatus, "Status is required.");
                isValid = false;
            }

            if (comboBoxCreatedBy.SelectedItem == null)
            {
                errorProvider.SetError(comboBoxCreatedBy, "Created by is required.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtBoxNotes.Text))
            {
                errorProvider.SetError(txtBoxNotes, "Notes are required.");
                isValid = false;
            }

            // Enable or disable the Save button based on the validation result
            btnSave.Enabled = isValid;
            btnSave.BackColor = isValid ? ColorTranslator.FromHtml("#05982E") : SystemColors.Control;
        }

    }
}
