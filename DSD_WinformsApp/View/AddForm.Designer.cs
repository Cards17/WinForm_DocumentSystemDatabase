namespace DSD_WinformsApp.View
{
    partial class AddForm
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
            txtFileName = new Label();
            textBoxFilename = new TextBox();
            txtCategory = new Label();
            Status_Label = new Label();
            cmbStatus = new ComboBox();
            Notes_Label = new Label();
            txtBoxNotes = new TextBox();
            cmbCategories = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            label1 = new Label();
            buttonUploadFile = new Button();
            labelFileUpload = new Label();
            SuspendLayout();
            // 
            // txtFileName
            // 
            txtFileName.AutoSize = true;
            txtFileName.Location = new Point(21, 86);
            txtFileName.Name = "txtFileName";
            txtFileName.Size = new Size(82, 25);
            txtFileName.TabIndex = 0;
            txtFileName.Text = "Filename";
            // 
            // textBoxFilename
            // 
            textBoxFilename.Location = new Point(122, 80);
            textBoxFilename.Name = "textBoxFilename";
            textBoxFilename.Size = new Size(227, 31);
            textBoxFilename.TabIndex = 1;
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
            cmbStatus.Location = new Point(122, 224);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(227, 33);
            cmbStatus.TabIndex = 5;
            // 
            // Notes_Label
            // 
            Notes_Label.AutoSize = true;
            Notes_Label.Location = new Point(415, 153);
            Notes_Label.Name = "Notes_Label";
            Notes_Label.Size = new Size(59, 25);
            Notes_Label.TabIndex = 6;
            Notes_Label.Text = "Notes";
            // 
            // txtBoxNotes
            // 
            txtBoxNotes.Location = new Point(492, 145);
            txtBoxNotes.Name = "txtBoxNotes";
            txtBoxNotes.Size = new Size(227, 31);
            txtBoxNotes.TabIndex = 7;
            // 
            // cmbCategories
            // 
            cmbCategories.FormattingEnabled = true;
            cmbCategories.Location = new Point(122, 145);
            cmbCategories.Name = "cmbCategories";
            cmbCategories.Size = new Size(227, 33);
            cmbCategories.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(563, 363);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(112, 34);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(424, 363);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 34);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(415, 86);
            label1.Name = "label1";
            label1.Size = new Size(59, 25);
            label1.TabIndex = 10;
            label1.Text = "Notes";
            // 
            // buttonUploadFile
            // 
            buttonUploadFile.Location = new Point(492, 80);
            buttonUploadFile.Name = "buttonUploadFile";
            buttonUploadFile.Size = new Size(110, 34);
            buttonUploadFile.TabIndex = 11;
            buttonUploadFile.Text = "Upload File";
            buttonUploadFile.UseVisualStyleBackColor = true;
            buttonUploadFile.Click += buttonUploadFile_Click;
            // 
            // labelFileUpload
            // 
            labelFileUpload.AutoSize = true;
            labelFileUpload.Location = new Point(616, 83);
            labelFileUpload.Name = "labelFileUpload";
            labelFileUpload.Size = new Size(107, 25);
            labelFileUpload.TabIndex = 12;
            labelFileUpload.Text = "dsadasdasd";
            // 
            // AddForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelFileUpload);
            Controls.Add(buttonUploadFile);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(cmbCategories);
            Controls.Add(txtBoxNotes);
            Controls.Add(Notes_Label);
            Controls.Add(cmbStatus);
            Controls.Add(Status_Label);
            Controls.Add(txtCategory);
            Controls.Add(textBoxFilename);
            Controls.Add(txtFileName);
            Name = "AddForm";
            Text = "AddForm";
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Label txtFileName;
        private TextBox Filename_Textbox;
        private Label txtCategory;
        private Label Status_Label;
        private ComboBox cmbStatus;
        private Label Notes_Label;
        private TextBox txtNotes;
        private ComboBox cmbCategories;
        private Button btnSave;
        private Button btnCancel;
        private TextBox textBoxFilename;
        private TextBox txtBoxNotes;
        private Label label1;
        private Button buttonUploadFile;
        private Label labelFileUpload;
    }
}