namespace DSD_WinformsApp.View
{
    partial class AddFormView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFormView));
            txtCategory = new Label();
            Status_Label = new Label();
            cmbStatus = new ComboBox();
            Notes_Label = new Label();
            txtBoxNotes = new TextBox();
            cmbCategories = new ComboBox();
            label1 = new Label();
            labelCreatedBy = new Label();
            comboBoxCreatedBy = new ComboBox();
            labelDocVersion = new Label();
            textBoxDocumentVersion = new TextBox();
            labelFilename = new Label();
            labelFileUpload = new Label();
            btnSave = new CustomButton(ColorTranslator.FromHtml("#05982E"), SystemColors.Control);
            btnCancel = new CustomButton(ColorTranslator.FromHtml("#DA0B0B"), SystemColors.Control);
            buttonUploadFile = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);
            SuspendLayout();
            // 
            // txtCategory
            // 
            txtCategory.AutoSize = true;
            txtCategory.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtCategory.Location = new Point(25, 204);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(192, 28);
            txtCategory.TabIndex = 2;
            txtCategory.Text = "Document Category:";
            // 
            // Status_Label
            // 
            Status_Label.AutoSize = true;
            Status_Label.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Status_Label.Location = new Point(25, 264);
            Status_Label.Name = "Status_Label";
            Status_Label.Size = new Size(165, 28);
            Status_Label.TabIndex = 4;
            Status_Label.Text = "Document Status:";
            // 
            // cmbStatus
            // 
            cmbStatus.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(239, 256);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(250, 36);
            cmbStatus.TabIndex = 5;
            // 
            // Notes_Label
            // 
            Notes_Label.AutoSize = true;
            Notes_Label.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Notes_Label.Location = new Point(519, 144);
            Notes_Label.Name = "Notes_Label";
            Notes_Label.Size = new Size(68, 28);
            Notes_Label.TabIndex = 6;
            Notes_Label.Text = "Notes:";
            // 
            // txtBoxNotes
            // 
            txtBoxNotes.Location = new Point(519, 196);
            txtBoxNotes.MaxLength = 300;
            txtBoxNotes.Multiline = true;
            txtBoxNotes.Name = "txtBoxNotes";
            txtBoxNotes.Size = new Size(331, 156);
            txtBoxNotes.TabIndex = 7;
            // 
            // cmbCategories
            // 
            cmbCategories.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cmbCategories.FormattingEnabled = true;
            cmbCategories.ItemHeight = 28;
            cmbCategories.Location = new Point(239, 196);
            cmbCategories.MaximumSize = new Size(250, 0);
            cmbCategories.Name = "cmbCategories";
            cmbCategories.Size = new Size(250, 36);
            cmbCategories.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(25, 25);
            label1.Name = "label1";
            label1.Size = new Size(174, 28);
            label1.TabIndex = 10;
            label1.Text = "Document Details";
            // 
            // labelCreatedBy
            // 
            labelCreatedBy.AutoSize = true;
            labelCreatedBy.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelCreatedBy.Location = new Point(27, 324);
            labelCreatedBy.Name = "labelCreatedBy";
            labelCreatedBy.Size = new Size(110, 28);
            labelCreatedBy.TabIndex = 12;
            labelCreatedBy.Text = "Created By:";
            // 
            // comboBoxCreatedBy
            // 
            comboBoxCreatedBy.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            comboBoxCreatedBy.FormattingEnabled = true;
            comboBoxCreatedBy.Location = new Point(239, 316);
            comboBoxCreatedBy.Name = "comboBoxCreatedBy";
            comboBoxCreatedBy.Size = new Size(250, 36);
            comboBoxCreatedBy.TabIndex = 15;
            // 
            // labelDocVersion
            // 
            labelDocVersion.AutoSize = true;
            labelDocVersion.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelDocVersion.Location = new Point(25, 144);
            labelDocVersion.Name = "labelDocVersion";
            labelDocVersion.Size = new Size(176, 28);
            labelDocVersion.TabIndex = 16;
            labelDocVersion.Text = "Document Version:";
            // 
            // textBoxDocumentVersion
            // 
            textBoxDocumentVersion.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxDocumentVersion.Location = new Point(239, 136);
            textBoxDocumentVersion.Multiline = true;
            textBoxDocumentVersion.Name = "textBoxDocumentVersion";
            textBoxDocumentVersion.Size = new Size(250, 36);
            textBoxDocumentVersion.TabIndex = 17;
            // 
            // labelFilename
            // 
            labelFilename.AutoSize = true;
            labelFilename.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFilename.Location = new Point(402, 86);
            labelFilename.Name = "labelFilename";
            labelFilename.Size = new Size(0, 28);
            labelFilename.TabIndex = 18;
            // 
            // labelFileUpload
            // 
            labelFileUpload.AutoSize = true;
            labelFileUpload.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            labelFileUpload.Location = new Point(25, 84);
            labelFileUpload.Name = "labelFileUpload";
            labelFileUpload.Size = new Size(177, 28);
            labelFileUpload.TabIndex = 19;
            labelFileUpload.Text = "Upload Document:";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(5, 152, 46);
            btnSave.Cursor = Cursors.Hand;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnSave.Location = new Point(738, 416);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 36);
            btnSave.TabIndex = 22;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(218, 11, 11);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btnCancel.Location = new Point(551, 416);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 36);
            btnCancel.TabIndex = 21;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // buttonUploadFile
            // 
            buttonUploadFile.BackColor = Color.FromArgb(165, 215, 232);
            buttonUploadFile.Cursor = Cursors.Hand;
            buttonUploadFile.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            buttonUploadFile.Location = new Point(239, 78);
            buttonUploadFile.Name = "buttonUploadFile";
            buttonUploadFile.Size = new Size(112, 36);
            buttonUploadFile.TabIndex = 20;
            buttonUploadFile.Text = "Upload";
            buttonUploadFile.UseVisualStyleBackColor = true;
            buttonUploadFile.Click += buttonUploadFile_Click;
            // 
            // AddFormView
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 494);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(buttonUploadFile);
            Controls.Add(textBoxDocumentVersion);
            Controls.Add(labelDocVersion);
            Controls.Add(comboBoxCreatedBy);
            Controls.Add(labelFilename);
            Controls.Add(labelCreatedBy);
            Controls.Add(labelFileUpload);
            Controls.Add(label1);
            Controls.Add(cmbCategories);
            Controls.Add(txtBoxNotes);
            Controls.Add(Notes_Label);
            Controls.Add(cmbStatus);
            Controls.Add(Status_Label);
            Controls.Add(txtCategory);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddFormView";
            Text = "Add Document";
            Load += AddForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox Filename_Textbox;
        private Label txtCategory;
        private Label Status_Label;
        private ComboBox cmbStatus;
        private Label Notes_Label;
        private TextBox txtNotes;
        private ComboBox cmbCategories;
        private TextBox txtBoxNotes;
        private Label label1;
        private Label labelCreatedBy;
        private ComboBox comboBoxCreatedBy;
        private TextBox textBoxCreatedBy;
        private Label labelDocVersion;
        private TextBox textBoxDocumentVersion;
        private Label labelFilename;
        private Label labelFileUpload;
        private CustomButton btnCancel;
        private CustomButton btnSave;
        private CustomButton buttonUploadFile;

    }
}