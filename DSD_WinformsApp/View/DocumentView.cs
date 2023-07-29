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
    public partial class DocumentView : Form, IDocumentView
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DocumentPresenter _presenter;
        public DocumentView(IUnitOfWork unitOfWork)
        {
            InitializeComponent();
            _unitOfWork = unitOfWork;
            _presenter = new DocumentPresenter(this, _unitOfWork.Documents);

        }

        public void BindData(List<DocumentDto> documents)
        {
            dataGridView1.DataSource = documents;
        }

        private async void DocumentView_Load_1(object sender, EventArgs e)
        {
            // Loads all documents list from data source.
            await _presenter.LoadDocuments();
            
            // Define the column width from documentmodel
            dataGridView1.Columns["Id"].Width = 50;
            dataGridView1.Columns["Filename"].Width = 300;
            dataGridView1.Columns["Category"].Width = 200;
            dataGridView1.Columns["Status"].Width = 150;
            dataGridView1.Columns["CreatedDate"].Width = 200;

            // Add details button functionality
            DataGridViewButtonColumn detailsColumn = new DataGridViewButtonColumn();
            detailsColumn.Text = "Details";
            detailsColumn.Name = "Menu";
            detailsColumn.Width = 100;
            detailsColumn.UseColumnTextForButtonValue = true; // This is important to show the button text.
            dataGridView1.Columns.Add(detailsColumn);


            // Wire up the CellClick event handler
            dataGridView1.CellClick += dataGridView1_CellClick;

        }

        //Event method when details button was clicked
        private void dataGridView1_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Menu"].Index)
            {
                DocumentDto selectedDocument = (DocumentDto)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                ShowDocumentDetailsModal(selectedDocument);
            }
        }
        private void ShowDocumentDetailsModal(DocumentDto selectedDocument)
        {
            // Create a new form to display the document details (modal form).
            Form detailsForm = new Form();
            detailsForm.Text = "Document Details";
            detailsForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            detailsForm.StartPosition = FormStartPosition.CenterParent;

            // Create controls to display the document details.
            Label idLabel = new Label();
            idLabel.Text = "ID:";
            idLabel.Location = new Point(10, 10);

            Label filenameLabel = new Label();
            filenameLabel.Text = "Filename:";
            filenameLabel.Location = new Point(10, 40);

            Label categoryLabel = new Label();
            categoryLabel.Text = "Category:";
            categoryLabel.Location = new Point(10, 70);

            Label statusLabel = new Label();
            statusLabel.Text = "Status:";
            statusLabel.Location = new Point(10, 100);

            Label createdDateLabel = new Label();
            createdDateLabel.Text = "Created Date:";
            createdDateLabel.Location = new Point(10, 130);

            TextBox idTextBox = new TextBox();
            idTextBox.Text = selectedDocument.Id.ToString();
            idTextBox.Location = new Point(120, 10);
            idTextBox.ReadOnly = true;

            TextBox filenameTextBox = new TextBox();
            filenameTextBox.Text = selectedDocument.Filename;
            filenameTextBox.Location = new Point(120, 40);
            filenameTextBox.ReadOnly = true;

            TextBox categoryTextBox = new TextBox();
            categoryTextBox.Text = selectedDocument.Category;
            categoryTextBox.Location = new Point(120, 70);
            categoryTextBox.ReadOnly = true;

            TextBox statusTextBox = new TextBox();
            statusTextBox.Text = selectedDocument.Status;
            statusTextBox.Location = new Point(120, 100);
            statusTextBox.ReadOnly = true;

            TextBox createdDateTextBox = new TextBox();
            createdDateTextBox.Text = selectedDocument.CreatedDate.ToString("yyyy-MM-dd");
            createdDateTextBox.Location = new Point(120, 130);
            createdDateTextBox.ReadOnly = true;

            // Add controls to the modal form.
            detailsForm.Controls.Add(idLabel);
            detailsForm.Controls.Add(filenameLabel);
            detailsForm.Controls.Add(categoryLabel);
            detailsForm.Controls.Add(statusLabel);
            detailsForm.Controls.Add(createdDateLabel);
            detailsForm.Controls.Add(idTextBox);
            detailsForm.Controls.Add(filenameTextBox);
            detailsForm.Controls.Add(categoryTextBox);
            detailsForm.Controls.Add(statusTextBox);
            detailsForm.Controls.Add(createdDateTextBox);

            // Set the size of the modal form.
            detailsForm.ClientSize = new Size(320, 180);

            // Display the form as a modal dialog, which means the user cannot interact with the main form until this form is closed.
            detailsForm.ShowDialog();
        }
    }
}
