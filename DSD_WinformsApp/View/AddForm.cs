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
        private byte[] uploadedFileData = null!; // Store the uploaded file data as a byte array

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
            // Create an instance of DocumentDto to hold the data
            var documentDto = new DocumentDto
            {
                Filename = textBoxFilename.Text,
                Category = cmbCategories.SelectedItem?.ToString() ?? "DefaultCategory",
                Status = cmbStatus.SelectedItem?.ToString() ?? "DefaultStatus",
                Notes = txtBoxNotes.Text,

                // update this code!! to upload file to database or filesystem.
                // idea ensure to update database when changing properties in the model
                // FileData = uploadedFileData // Set the file data to the DocumentDto

            };
            // Use the presenter to save the document 
            _presenter.SaveDocument(documentDto);

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
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Read the file data into the uploadedFileData byte array
                    uploadedFileData = File.ReadAllBytes(openFileDialog.FileName);

                    // Display the selected file name in the label
                    labelFileUpload.Text = openFileDialog.SafeFileName;
                }
            }

        }
    }
}
