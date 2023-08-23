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
            labelFileUpload = new Label();
            labelCreatedBy = new Label();
            labelFilename = new Label();
            comboBoxCreatedBy = new ComboBox();
            btnSave = new CustomButton(ColorTranslator.FromHtml("#05982E"), SystemColors.Control);
            btnCancel = new CustomButton(ColorTranslator.FromHtml("#DA0B0B"), SystemColors.Control);
            buttonUploadFile = new CustomButton(ColorTranslator.FromHtml("#A5D7E8"), SystemColors.Control);

            SuspendLayout();
            // 
            // txtCategory
            // 
            txtCategory.AutoSize = true;
            txtCategory.Location = new Point(21, 153);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(84, 25);
            txtCategory.TabIndex = 2;
            txtCategory.Text = "Category";
            // 
            // Status_Label
            // 
            Status_Label.AutoSize = true;
            Status_Label.Location = new Point(21, 227);
            Status_Label.Name = "Status_Label";
            Status_Label.Size = new Size(101, 25);
            Status_Label.TabIndex = 4;
            Status_Label.Text = "Doc. Status";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(127, 224);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(227, 33);
            cmbStatus.TabIndex = 5;
            // 
            // Notes_Label
            // 
            Notes_Label.AutoSize = true;
            Notes_Label.Location = new Point(364, 148);
            Notes_Label.Name = "Notes_Label";
            Notes_Label.Size = new Size(59, 25);
            Notes_Label.TabIndex = 6;
            Notes_Label.Text = "Notes";
            // 
            // txtBoxNotes
            // 
            txtBoxNotes.Location = new Point(426, 145);
            txtBoxNotes.MaxLength = 300;
            txtBoxNotes.Multiline = true;
            txtBoxNotes.Name = "txtBoxNotes";
            txtBoxNotes.Size = new Size(331, 174);
            txtBoxNotes.TabIndex = 7;
            // 
            // cmbCategories
            // 
            cmbCategories.FormattingEnabled = true;
            cmbCategories.Location = new Point(127, 145);
            cmbCategories.Name = "cmbCategories";
            cmbCategories.Size = new Size(227, 33);
            cmbCategories.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 80);
            label1.Name = "label1";
            label1.Size = new Size(95, 25);
            label1.TabIndex = 10;
            label1.Text = "Document";
            // 
            // labelFileUpload
            // 
            labelFileUpload.Location = new Point(0, 0);
            labelFileUpload.Name = "labelFileUpload";
            labelFileUpload.Size = new Size(100, 23);
            labelFileUpload.TabIndex = 0;
            // 
            // labelCreatedBy
            // 
            labelCreatedBy.AutoSize = true;
            labelCreatedBy.Location = new Point(23, 292);
            labelCreatedBy.Name = "labelCreatedBy";
            labelCreatedBy.Size = new Size(97, 25);
            labelCreatedBy.TabIndex = 12;
            labelCreatedBy.Text = "Created By";
            // 
            // labelFilename
            // 
            labelFilename.AutoSize = true;
            labelFilename.Location = new Point(257, 79);
            labelFilename.Name = "labelFilename";
            labelFilename.Size = new Size(0, 25);
            labelFilename.TabIndex = 14;
            // 
            // comboBoxCreatedBy
            // 
            comboBoxCreatedBy.FormattingEnabled = true;
            comboBoxCreatedBy.Location = new Point(128, 284);
            comboBoxCreatedBy.Name = "comboBoxCreatedBy";
            comboBoxCreatedBy.Size = new Size(226, 33);
            comboBoxCreatedBy.TabIndex = 15;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(5, 152, 46);
            btnSave.Location = new Point(645, 363);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 34);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = SystemColors.ActiveBorder;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Location = new Point(490, 363);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 34);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // buttonUploadFile
            // 
            buttonUploadFile.BackColor = SystemColors.ControlLight;
            buttonUploadFile.Cursor = Cursors.Hand;
            buttonUploadFile.Location = new Point(128, 74);
            buttonUploadFile.Name = "buttonUploadFile";
            buttonUploadFile.Size = new Size(110, 34);
            buttonUploadFile.TabIndex = 11;
            buttonUploadFile.Text = "Upload File";
            buttonUploadFile.UseVisualStyleBackColor = false;
            buttonUploadFile.Click += buttonUploadFile_Click;
            // 
            // AddFormView
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
            Controls.Add(buttonUploadFile);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
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
        private Label labelFileUpload;
        private Label labelCreatedBy;
        private CustomButton btnSave;
        private CustomButton btnCancel;
        private CustomButton buttonUploadFile;
        private Label labelFilename;
        private ComboBox comboBoxCreatedBy;
        private TextBox textBoxCreatedBy;
    }
}