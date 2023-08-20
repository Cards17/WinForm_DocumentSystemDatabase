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
        private readonly UserCredentialsDto _loggedInUser;

        private ErrorProvider errorProvider = null!; // Class-level variable to store the ErrorProvider component

        private string selectedFilePath = null!; // Class-level variable to store the selected file path


        public AddFormView(IUnitOfWork unitOfWork, IDocumentPresenter presenter, UserCredentialsDto userCredentials)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _presenter = presenter;
            _loggedInUser = userCredentials;


            MaximizeBox = false; // Remove the maximize box
            MinimizeBox = false; // Remove the minimize box

            errorProvider = new ErrorProvider();  // Initialize the ErrorProvider component

            // Initialize the ComboBox controls
            StatusComboBox();
            CategoryComboBox();
        }


        private void AddForm_Load(object sender, EventArgs e)
        {
            // Set CustomButton properties
            //btnSave = new CustomButton(ColorTranslator.FromHtml("#05982E"), SystemColors.Control);
            //btnCancel = new CustomButton(ColorTranslator.FromHtml("#DA0B0B"), SystemColors.Control);
            //buttonUploadFile = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);

            // Set the title of the form
            MaximizeBox = false;

            // Disable the Save button initially
            btnSave.Enabled = false;

            // Hide filename label initially
            labelFilename.Visible = false;

            // Attach TextChanged event handlers to relevant controls
            labelFilename.TextChanged += Control_TextChanged;
            txtBoxNotes.TextChanged += Control_TextChanged;
            textBoxCreatedBy.TextChanged += Control_TextChanged;

            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink; // remove the blinking icon when error occurs

            // Attach SelectedIndexChanged event handlers to ComboBox controls
            cmbCategories.SelectedIndexChanged += Control_SelectedIndexChanged;
            cmbStatus.SelectedIndexChanged += Control_SelectedIndexChanged;

        }

        // Status selection
        private void StatusComboBox()
        {
            cmbStatus.Items.Add("Open");
            cmbStatus.Items.Add("In Progress");
            cmbStatus.Items.Add("Closed");
        }

        // Category selection
        private void CategoryComboBox()
        {
            cmbCategories.Items.Add("Category 1");
            cmbCategories.Items.Add("Category 2");
            cmbCategories.Items.Add("Category 3");
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // Check if a file is selected
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please select a file before saving.");
                return;
            }


            // Read the file data from the selected file
            byte[] fileDataBytes = File.ReadAllBytes(selectedFilePath);


            // Access user credentials from parameter  passed to the form
            // var userName = userCredentials.Firstname + " " + userCredentials.Lastname;


            // Create an instance of DocumentDto to hold the data
            var documentDto = new DocumentDto
            {
                Filename = labelFilename.Text,
                Category = cmbCategories.SelectedItem?.ToString() ?? "Select Category",
                Status = cmbStatus.SelectedItem?.ToString() ?? "Select Status",
                Notes = txtBoxNotes.Text,
                CreatedBy = textBoxCreatedBy.Text,
                CreatedDate = DateTime.Now.Date,
                ModifiedDate = DateTime.Now.Date,

            };



            // Use the presenter to save the document with file data
            _presenter.SaveDocument(documentDto, fileDataBytes);

            // Load the documents again to update the view
            await _presenter.LoadDocuments();

            // Close the form and return DialogResult.OK
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close the form and return DialogResult.Cancel
            DialogResult = DialogResult.Cancel;
        }

        private void buttonUploadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedExtension = Path.GetExtension(openFileDialog.FileName); // Get the selected file extension
                List<string> allowedExtensions = new List<string> { ".docx", ".doc", ".xlsx", ".xls", ".pdf" }; // Allowed file extensions
                if (!allowedExtensions.Contains(selectedExtension))
                {
                    MessageBox.Show("Please select a valid file type.");
                    return;
                }

                selectedFilePath = openFileDialog.FileName; // Store the selected file path

                // Display only the file name without the extension in the label and the TextBox
                string fileNameWithExtension = Path.GetFileName(openFileDialog.FileName);
                labelFilename.Text = fileNameWithExtension;
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
                errorProvider.SetError(buttonUploadFile, "Upload file is required.");
                isValid = false;
            }

            if (cmbCategories.SelectedItem == null)
            {
                errorProvider.SetError(cmbCategories, "Category is required.");
                isValid = false;
            }

            if (cmbStatus.SelectedItem == null)
            {
                errorProvider.SetError(cmbStatus, "Status is required.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtBoxNotes.Text))
            {
                errorProvider.SetError(txtBoxNotes, "Notes are required.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(textBoxCreatedBy.Text))
            {
                errorProvider.SetError(textBoxCreatedBy, "Created By is required.");
                isValid = false;
            }

            btnSave.Enabled = isValid;
        }

    }
}
