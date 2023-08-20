namespace DSD_WinformsApp.View
{
    partial class LoginFormView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginFormView));
            textBoxEmailAddress = new TextBox();
            textBoxPassword = new TextBox();
            button_SignIn = new Button();
            labelSignIn = new Label();
            linkLabelSignUp = new LinkLabel();
            linkLabelForgetPassword = new LinkLabel();
            panelSignIn = new Panel();
            panelSignUp = new Panel();
            buttonBackToSignIn = new Button();
            buttonSignUp = new Button();
            textBoxPasswrd = new TextBox();
            textBoxEmailAdd = new TextBox();
            textBoxLastname = new TextBox();
            labelSIgnUp = new Label();
            textBoxFirstname = new TextBox();
            panelSignIn.SuspendLayout();
            panelSignUp.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxEmailAddress
            // 
            textBoxEmailAddress.Location = new Point(104, 107);
            textBoxEmailAddress.Name = "textBoxEmailAddress";
            textBoxEmailAddress.PlaceholderText = "Email Address";
            textBoxEmailAddress.Size = new Size(350, 31);
            textBoxEmailAddress.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(104, 186);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.PlaceholderText = "Password";
            textBoxPassword.Size = new Size(350, 31);
            textBoxPassword.TabIndex = 3;
            // 
            // button_SignIn
            // 
            button_SignIn.BackColor = Color.FromArgb(165, 215, 232);
            button_SignIn.Cursor = Cursors.Hand;
            button_SignIn.Location = new Point(104, 258);
            button_SignIn.Name = "button_SignIn";
            button_SignIn.Size = new Size(350, 35);
            button_SignIn.TabIndex = 4;
            button_SignIn.Text = "Sign In";
            button_SignIn.UseVisualStyleBackColor = false;
            button_SignIn.Click += button_SignIn_Click;
            // 
            // labelSignIn
            // 
            labelSignIn.AutoSize = true;
            labelSignIn.Location = new Point(42, 27);
            labelSignIn.Name = "labelSignIn";
            labelSignIn.Size = new Size(67, 25);
            labelSignIn.TabIndex = 5;
            labelSignIn.Text = "Sign In";
            // 
            // linkLabelSignUp
            // 
            linkLabelSignUp.AutoSize = true;
            linkLabelSignUp.Location = new Point(104, 392);
            linkLabelSignUp.Name = "linkLabelSignUp";
            linkLabelSignUp.Size = new Size(138, 25);
            linkLabelSignUp.TabIndex = 6;
            linkLabelSignUp.TabStop = true;
            linkLabelSignUp.Text = "Click to Sign Up";
            // 
            // linkLabelForgetPassword
            // 
            linkLabelForgetPassword.AutoSize = true;
            linkLabelForgetPassword.Location = new Point(104, 337);
            linkLabelForgetPassword.Name = "linkLabelForgetPassword";
            linkLabelForgetPassword.Size = new Size(152, 25);
            linkLabelForgetPassword.TabIndex = 7;
            linkLabelForgetPassword.TabStop = true;
            linkLabelForgetPassword.Text = "Forget Password?";
            // 
            // panelSignIn
            // 
            panelSignIn.Controls.Add(panelSignUp);
            panelSignIn.Location = new Point(26, 12);
            panelSignIn.Name = "panelSignIn";
            panelSignIn.Size = new Size(539, 454);
            panelSignIn.TabIndex = 8;
            // 
            // panelSignUp
            // 
            panelSignUp.Controls.Add(buttonBackToSignIn);
            panelSignUp.Controls.Add(buttonSignUp);
            panelSignUp.Controls.Add(textBoxPasswrd);
            panelSignUp.Controls.Add(textBoxEmailAdd);
            panelSignUp.Controls.Add(textBoxLastname);
            panelSignUp.Controls.Add(labelSIgnUp);
            panelSignUp.Controls.Add(textBoxFirstname);
            panelSignUp.Location = new Point(0, 0);
            panelSignUp.Name = "panelSignUp";
            panelSignUp.Size = new Size(539, 454);
            panelSignUp.TabIndex = 0;
            // 
            // buttonBackToSignIn
            // 
            buttonBackToSignIn.Cursor = Cursors.Hand;
            buttonBackToSignIn.Location = new Point(46, 380);
            buttonBackToSignIn.Name = "buttonBackToSignIn";
            buttonBackToSignIn.Size = new Size(442, 34);
            buttonBackToSignIn.TabIndex = 6;
            buttonBackToSignIn.Text = "Back";
            buttonBackToSignIn.UseVisualStyleBackColor = true;
            buttonBackToSignIn.Click += buttonBackToSignIn_Click;
            // 
            // buttonSignUp
            // 
            buttonSignUp.Cursor = Cursors.Hand;
            buttonSignUp.Location = new Point(46, 334);
            buttonSignUp.Name = "buttonSignUp";
            buttonSignUp.Size = new Size(442, 34);
            buttonSignUp.TabIndex = 5;
            buttonSignUp.Text = "Sign Up";
            buttonSignUp.UseVisualStyleBackColor = true;
            buttonSignUp.Click += buttonSignUp_Click;
            // 
            // textBoxPasswrd
            // 
            textBoxPasswrd.Location = new Point(48, 271);
            textBoxPasswrd.Name = "textBoxPasswrd";
            textBoxPasswrd.PlaceholderText = "Password";
            textBoxPasswrd.Size = new Size(442, 31);
            textBoxPasswrd.TabIndex = 4;
            // 
            // textBoxEmailAdd
            // 
            textBoxEmailAdd.Location = new Point(48, 212);
            textBoxEmailAdd.Name = "textBoxEmailAdd";
            textBoxEmailAdd.PlaceholderText = "Email Address";
            textBoxEmailAdd.Size = new Size(442, 31);
            textBoxEmailAdd.TabIndex = 3;
            // 
            // textBoxLastname
            // 
            textBoxLastname.Location = new Point(46, 151);
            textBoxLastname.Name = "textBoxLastname";
            textBoxLastname.PlaceholderText = "Last Name";
            textBoxLastname.Size = new Size(442, 31);
            textBoxLastname.TabIndex = 2;
            // 
            // labelSIgnUp
            // 
            labelSIgnUp.AutoSize = true;
            labelSIgnUp.Location = new Point(46, 42);
            labelSIgnUp.Name = "labelSIgnUp";
            labelSIgnUp.Size = new Size(70, 25);
            labelSIgnUp.TabIndex = 0;
            labelSIgnUp.Text = "SignUp";
            // 
            // textBoxFirstname
            // 
            textBoxFirstname.Location = new Point(46, 95);
            textBoxFirstname.Name = "textBoxFirstname";
            textBoxFirstname.PlaceholderText = "First Name";
            textBoxFirstname.Size = new Size(442, 31);
            textBoxFirstname.TabIndex = 1;
            // 
            // LoginFormView
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 600);
            Controls.Add(panelSignIn);
            Controls.Add(linkLabelForgetPassword);
            Controls.Add(linkLabelSignUp);
            Controls.Add(labelSignIn);
            Controls.Add(button_SignIn);
            Controls.Add(textBoxPassword);
            Controls.Add(textBoxEmailAddress);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginFormView";
            Text = "Login Page";
            Load += LoginFormView_Load;
            panelSignIn.ResumeLayout(false);
            panelSignUp.ResumeLayout(false);
            panelSignUp.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxEmailAddress;
        private TextBox textBoxPassword;
        private Button button_SignIn;
        private Label labelSignIn;
        private LinkLabel linkLabelSignUp;
        private LinkLabel linkLabelForgetPassword;
        private Panel panelSignIn;
        private Panel panelSignUp;
        private TextBox textBoxFirstname;
        private Label labelSIgnUp;
        private TextBox textBoxPasswrd;
        private TextBox textBoxEmailAdd;
        private TextBox textBoxLastname;
        private Button buttonSignUp;
        private Label labelToastMessage;
        private Button buttonBackToSignIn;
    }
}