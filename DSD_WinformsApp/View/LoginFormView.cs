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

        private ErrorProvider errorProvider = null!;

        public LoginFormView(IDocumentPresenter presenter, IDocumentView documentView)
        {
            InitializeComponent();
            _documentView = documentView;
            _presenter = presenter;

            errorProvider = new ErrorProvider(); // Initialize the ErrorProvider component

            StartPosition = FormStartPosition.CenterScreen; // Set the form's start position to the center of the screen

        }


        private void LoginFormView_Load(object sender, EventArgs e)
        {
            // Hide the signup panel initially
            panelSignUp.Visible = false;

            MaximizeBox = false; // Remove the maximize box
            MinimizeBox = false; // Remove the minimize box

            // Sign-up and sign-in button disabled initially
            buttonSignUp.Enabled = false;
            button_SignIn.Enabled = false;

            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink; // Icon will not blink when error occurs

            textBoxEmailAddress.Focus(); // Set the focus to the textBoxFirstname control

            //If sign-in panel is visible attach TextChanged event handlers to relevant controls
            textBoxEmailAddress.TextChanged += Control_TextChanged;
            textBoxPassword.TextChanged += Control_TextChanged;

            SignInUI(); // Call the SignInUI method
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
            // clear the textboxes
            textBoxFirstname.Text = "";
            textBoxLastname.Text = "";
            textBoxEmailAdd.Text = "";
            textBoxPasswrd.Text = "";

            panelSignUp.Visible = true; // Show the signup panel

            textBoxFirstname.Focus(); // Set the focus to the textBoxFirstname control

            buttonSignUp.Enabled = false; // Disable the signup button initially

            textBoxFirstname.TextChanged += Control_TextChanged;
            textBoxLastname.TextChanged += Control_TextChanged;
            textBoxEmailAdd.TextChanged += Control_TextChanged;
            textBoxPasswrd.TextChanged += Control_TextChanged;

            // if paneSignup is visible remove the error icon from panelSignIn
            if (panelSignUp.Visible == true)
            {
                errorProvider.SetError(textBoxEmailAddress, "");
                errorProvider.SetError(textBoxPassword, "");
            }
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

                // Show confirmation modal and show to signin panel
                MessageBox.Show("Registration successful.");
                panelSignUp.Visible = false;

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
                // Disable signin panel page
                panelSignIn.Enabled = false;

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

                    button_SignIn.Enabled = false; //Disable the signin button
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

        private void Control_TextChanged(object? sender, EventArgs e)
        {
            ValidateForm();
        }

        private void ValidateForm()
        {
            bool isValid = true;
            errorProvider.Clear();

            if (panelSignIn.Visible && !panelSignUp.Visible)
            {
                // add the email and password validation
                if (string.IsNullOrWhiteSpace(textBoxEmailAddress.Text))
                {
                    errorProvider.SetError(textBoxEmailAddress, "Email Address is required.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
                {
                    errorProvider.SetError(textBoxPassword, "Password is required.");
                    isValid = false;
                }
            }

            if (panelSignUp.Visible)
            {
                // Add the signup form validation
                if (string.IsNullOrWhiteSpace(textBoxFirstname.Text))
                {
                    errorProvider.SetError(textBoxFirstname, "First Name is required.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(textBoxLastname.Text))
                {
                    errorProvider.SetError(textBoxLastname, "Last Name is required.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(textBoxEmailAdd.Text))
                {
                    errorProvider.SetError(textBoxEmailAdd, "Email Address is required.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(textBoxPasswrd.Text))
                {
                    errorProvider.SetError(textBoxPasswrd, "Password is required.");
                    isValid = false;
                }
            }


            buttonSignUp.Enabled = isValid;
            button_SignIn.Enabled = isValid;
        }

        private void buttonBackToSignIn_Click(object sender, EventArgs e)
        {
            // Hide the signup panel
            panelSignUp.Visible = false;
            panelSignIn.Visible = true;

        }
    }



}
