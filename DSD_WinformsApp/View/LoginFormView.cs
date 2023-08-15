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
    public partial class LoginFormView : Form
    {
        public LoginFormView()
        {
            InitializeComponent();

            MaximizeBox = false; // Remove the maximize box
            MinimizeBox = false; // Remove the minimize box

            SignInUI();
        }


        private void LoginFormView_Load(object sender, EventArgs e)
        {

        }

        private void SignInUI()
        {
            // Add controls to the panelSignIn
            panelSignIn.Controls.Add(textBoxEmailAddress);
            panelSignIn.Controls.Add(textBoxPassword);
            panelSignIn.Controls.Add(button_SignIn);
            panelSignIn.Controls.Add(labelSignIn);
            panelSignIn.Controls.Add(linkLabelSignUp);
            panelSignIn.Controls.Add(linkLabelForgetPassword);

            // Add controls to the panelSignUp
            panelSignUp.Controls.Add(labelSIgnUp);
            panelSignUp.Controls.Add(textBoxFirstname);
            panelSignUp.Controls.Add(textBoxLastname);
            panelSignUp.Controls.Add(textBoxEmailAdd);
            panelSignUp.Controls.Add(textBoxPasswrd);
            panelSignUp.Controls.Add(buttonSignUp);


            // Hide the signup panel initially
            panelSignUp.Visible = false;

            // Set hex color code signin and signup button
            button_SignIn.BackColor = ColorTranslator.FromHtml("#A5D7E8");
            buttonSignUp.BackColor = ColorTranslator.FromHtml("#05982E");

            // Wire up the linkLabelSignUp's Click event to show the signup panel
            linkLabelSignUp.Click += LinkLabelSignUp_Click;
        }

        private void LinkLabelSignUp_Click(object? sender, EventArgs e)
        {
            // Show the signup panel
            panelSignUp.Visible = true;
        }

    }
}
