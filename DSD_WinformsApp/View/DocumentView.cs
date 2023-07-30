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
            dataGridView1.Columns["Id"].Width = 60;
            dataGridView1.Columns["Filename"].Width = 300;
            dataGridView1.Columns["Category"].Width = 200;
            dataGridView1.Columns["Status"].Width = 150;
            dataGridView1.Columns["CreatedDate"].Width = 200;
            dataGridView1.Columns["CreatedBy"].Visible = false;
            dataGridView1.Columns["ModifiedBy"].Visible = false;
            dataGridView1.Columns["ModifiedDate"].Visible = false;
            dataGridView1.Columns["Notes"].Visible = false;


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
                    DeleteDocument(selectedDocument);
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
            Form1 detailsForm = new Form1();

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

            // Add controls to display the document details.
            AddRow(groupBox, "ID:", new TextBox { Text = selectedDocument.Id.ToString(), ReadOnly = true });
            AddRow(groupBox, "Filename:", new TextBox { Text = selectedDocument.Filename, ReadOnly = true });
            AddRow(groupBox, "Category:", new TextBox { Text = selectedDocument.Category, ReadOnly = true });
            AddRow(groupBox, "Status:", new TextBox { Text = selectedDocument.Status, ReadOnly = true });
            AddRow(groupBox, "Created Date:", new TextBox { Text = selectedDocument.CreatedDate.ToString("yyyy-MM-dd"), ReadOnly = true });

            // Add controls for the additional properties.
            AddRow(groupBox, "Created By:", new TextBox { Text = selectedDocument.CreatedBy, ReadOnly = true });
            AddRow(groupBox, "Modified By:", new TextBox { Text = selectedDocument.ModifiedBy, ReadOnly = true });
            AddRow(groupBox, "Modified Date:", new TextBox { Text = selectedDocument.ModifiedDate.ToString("yyyy-MM-dd"), ReadOnly = true });
            // Create a multiline TextBox for the "Notes" property
            TextBox notesTextBox = new TextBox();
            notesTextBox.Text = selectedDocument.Notes;
            notesTextBox.ReadOnly = true;
            notesTextBox.Multiline = true;
            notesTextBox.ScrollBars = ScrollBars.Vertical; // Optional: Set the scrollbar type (vertical scrollbar in this case).
            notesTextBox.MaxLength = 150;

            // Calculate the preferred height for the multiline TextBox based on the text content
            using (Graphics g = notesTextBox.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(notesTextBox.Text, notesTextBox.Font, notesTextBox.Width);
                int preferredHeight = (int)textSize.Height + 10; // Add some buffer to avoid cutting off the text.
                notesTextBox.Height = Math.Max(preferredHeight, 0); // Set a minimum height to prevent the TextBox from becoming too small.
            }

            AddRow(groupBox, "Notes:", notesTextBox);

            // Function to add a row (label + control) to the GroupBox
            void AddRow(GroupBox parent, string labelText, Control control)
            {
                Label label = new Label();
                label.Text = labelText;
                label.AutoSize = true;

                int labelTop = parent.Controls.Count * 20 + 40;
                label.Location = new Point(10, labelTop);

                int controlLeft = label.Right + 50;
                control.Location = new Point(controlLeft, labelTop);
                control.Width = parent.Width - controlLeft - 80;

                parent.Controls.Add(label);
                parent.Controls.Add(control);
            }

            // Adjust the size of the GroupBox to fit its contents
            int groupBoxWidth = 750;
            int groupBoxHeight = groupBox.Controls.Count * 30 + 30; // Add some buffer (e.g., 40 pixels) to avoid cutting off any controls.
            groupBox.Width = groupBoxWidth;
            groupBox.Height = groupBoxHeight;

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
            };

            // Handle the Edit button click event
            void EditButton_Click(object? sender, EventArgs e)
            {
                // Add the logic to handle the Edit button click here.
                // For example, you can enable editing of the controls inside the GroupBox.
            }

            // Handle the Close button click event
            void CloseButton_Click(object? sender, EventArgs e)
            {
                detailsForm.Close(); // Close the form when the Close button is clicked.
            }

            // Show the detailsForm
            detailsForm.ShowDialog();

        }







        private void DeleteDocument(DocumentDto document)
        {
            // Your logic to delete the document goes here.
            // For example, remove it from the data source or perform any necessary operations.
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
