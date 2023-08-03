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
    public partial class AddForm : Form
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DocumentPresenter _presenter;

        private string selectedFilePath = null!; // Class-level variable to store the selected file path


        public AddForm(IUnitOfWork unitOfWork, DocumentPresenter presenter)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _presenter = presenter;
            StatusComboBox();
            CategoryComboBox();
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
            cmbCategories.Items.Add("Type A");
            cmbCategories.Items.Add("Type B");
            cmbCategories.Items.Add("Type C");
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

            // Create an instance of DocumentDto to hold the data
            var documentDto = new DocumentDto
            {
                Filename = textBoxFilename.Text,
                Category = cmbCategories.SelectedItem?.ToString() ?? "Select Category",
                Status = cmbStatus.SelectedItem?.ToString() ?? "Select Status",
                Notes = txtBoxNotes.Text,
                CreatedBy = textBoxCreatedBy.Text,
                CreatedDate = DateTime.Now.Date,

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
                selectedFilePath = openFileDialog.FileName; // Store the selected file path

                // Display only the file name without the extension in the label and the TextBox
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                labelFileUpload.Text = fileNameWithoutExtension;
                textBoxFilename.Text = fileNameWithoutExtension;
            }


        }

        private void AddForm_Load(object sender, EventArgs e)
        {

        }
    }
}
