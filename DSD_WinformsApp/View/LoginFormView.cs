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
using System.Timers;
using System.Windows.Forms;

namespace DSD_WinformsApp.View
{
    public partial class LoginFormView : Form
    {
        private readonly IDocumentPresenter _presenter;
        private readonly IDocumentView _documentView;


        public LoginFormView(IDocumentPresenter presenter, IDocumentView documentView)
        {
            InitializeComponent();
            _documentView = documentView;
            _presenter = presenter;

            MaximizeBox = false; // Remove the maximize box
            MinimizeBox = false; // Remove the minimize box

            SignInUI();
        }


        private void LoginFormView_Load(object sender, EventArgs e)
        {
            // Hide the toast message label and signup panel initially
            labelToastMessage.Visible = false;
            panelSignUp.Visible = false;

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

            // clear the textboxes
            textBoxFirstname.Text = "";
            textBoxLastname.Text = "";
            textBoxEmailAdd.Text = "";
            textBoxPasswrd.Text = "";

        }

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the UserCredentialsDto
                var userCredentials = new UserCredentialsDto
                {
                    Firstname = textBoxFirstname.Text,
                    Lastname = textBoxLastname.Text,
                    EmailAddress = textBoxEmailAdd.Text,
                    Password = textBoxPasswrd.Text
                };

                // Use presenter to call the signup method
                _presenter.SaveUserRegistration(userCredentials);

                // Start a timer to handle the confirmation message and panel visibility
                var confirmationTimer = new System.Windows.Forms.Timer();
                confirmationTimer.Interval = 1000; // 1 second
                confirmationTimer.Tick += (timerSender, timerArgs) =>
                {
                    confirmationTimer.Stop();
                    buttonSignUp.Enabled = false; // Disable the signup button
                    panelSignIn.Visible = true;   // Show the signin panel
                    panelSignUp.Visible = false;  // Hide the signup panel
                };

                // Start the timer
                confirmationTimer.Start();


            }
            catch (Exception ex)
            {
                // Display error message
                MessageBox.Show(ex.Message);
            }

        }

        private async void button_SignIn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the UserCredentialsDto
                var userCredentials = new UserCredentialsDto
                {
                    EmailAddress = textBoxEmailAddress.Text,
                    Password = textBoxPassword.Text
                };

                // Use presenter to call the signin method asynchronously
                bool isValidCredentials = await _presenter.ValidateUserCredentials(userCredentials);

                if (isValidCredentials)
                {
                    // Close the login form and show the document view
                    this.Hide();

                    _documentView.ShowDocumentView();
                }
                else
                {
                    // Show an error message for invalid credentials
                    MessageBox.Show("Invalid credentials. Please try again.");
                }
            }
            catch (Exception ex)
            {
                // Display error message
                MessageBox.Show(ex.Message);
            }
        }
    }

       
    
}
