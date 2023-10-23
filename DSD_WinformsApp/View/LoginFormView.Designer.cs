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
            textBoxSignInUserName = new TextBox();
            textBoxSignInPassword = new TextBox();
            button_SignInButton = new Button();
            labelSignIn = new Label();
            linkLabelSignInToSignUp = new LinkLabel();
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
            // textBoxSignInUserName
            // 
            textBoxSignInUserName.Location = new Point(104, 107);
            textBoxSignInUserName.Name = "textBoxSignInUserName";
            textBoxSignInUserName.PlaceholderText = "User Name";
            textBoxSignInUserName.Size = new Size(350, 31);
            textBoxSignInUserName.TabIndex = 2;
            // 
            // textBoxSignInPassword
            // 
            textBoxSignInPassword.Location = new Point(104, 186);
            textBoxSignInPassword.Name = "textBoxSignInPassword";
            textBoxSignInPassword.PlaceholderText = "Password";
            textBoxSignInPassword.Size = new Size(350, 31);
            textBoxSignInPassword.TabIndex = 3;
            // 
            // button_SignInButton
            // 
            button_SignInButton.BackColor = Color.FromArgb(165, 215, 232);
            button_SignInButton.Cursor = Cursors.Hand;
            button_SignInButton.Location = new Point(104, 258);
            button_SignInButton.Name = "button_SignInButton";
            button_SignInButton.Size = new Size(350, 35);
            button_SignInButton.TabIndex = 4;
            button_SignInButton.Text = "Sign In";
            button_SignInButton.UseVisualStyleBackColor = false;
            button_SignInButton.Click += button_SignIn_Click;
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
            // linkLabelSignInToSignUp
            // 
            linkLabelSignInToSignUp.AutoSize = true;
            linkLabelSignInToSignUp.Location = new Point(104, 318);
            linkLabelSignInToSignUp.Name = "linkLabelSignInToSignUp";
            linkLabelSignInToSignUp.Size = new Size(138, 25);
            linkLabelSignInToSignUp.TabIndex = 6;
            linkLabelSignInToSignUp.TabStop = true;
            linkLabelSignInToSignUp.Text = "Click to Sign Up";
            // 
            // panelSignIn
            // 
            panelSignIn.Controls.Add(linkLabelSignInToSignUp);
            panelSignIn.Controls.Add(panelSignUp);
            panelSignIn.Controls.Add(labelSignIn);
            panelSignIn.Controls.Add(button_SignInButton);
            panelSignIn.Controls.Add(textBoxSignInPassword);
            panelSignIn.Controls.Add(textBoxSignInUserName);
            panelSignIn.Location = new Point(30, 35);
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
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginFormView";
            Text = "Login Page";
            Load += LoginFormView_Load;
            panelSignIn.ResumeLayout(false);
            panelSignIn.PerformLayout();
            panelSignUp.ResumeLayout(false);
            panelSignUp.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxSignInUserName;
        private TextBox textBoxSignInPassword;
        private Button button_SignInButton;
        private Label labelSignIn;
        private LinkLabel linkLabelSignInToSignUp;
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