﻿using DSD_WinformsApp.Core.DTOs;
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
            button_SignInButton.Enabled = false;

            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink; // Icon will not blink when error occurs

            textBoxSignInUserName.Focus(); // Set the focus to the textBoxFirstname control

            //If sign-in panel is visible attach TextChanged event handlers to relevant controls
            textBoxSignInUserName.TextChanged += Control_TextChanged;
            textBoxSignInPassword.TextChanged += Control_TextChanged;

            SignInUI(); // Call the SignInUI method
        }

        private void SignInUI()
        {
            // Add controls to the panelSignIn
            panelSignIn.Controls.Add(textBoxSignInUserName);
            panelSignIn.Controls.Add(textBoxSignInPassword);
            panelSignIn.Controls.Add(button_SignInButton);
            panelSignIn.Controls.Add(labelSignIn);
            panelSignIn.Controls.Add(linkLabelSignInToSignUp);

            // Add controls to the panelSignUp
            panelSignUp.Controls.Add(labelSIgnUp);
            panelSignUp.Controls.Add(textBoxFirstname);
            panelSignUp.Controls.Add(textBoxLastname);
            panelSignUp.Controls.Add(textBoxEmailAdd);
            panelSignUp.Controls.Add(textBoxPasswrd);
            panelSignUp.Controls.Add(buttonSignUp);


            // Set hex color code signin and signup button
            button_SignInButton.BackColor = ColorTranslator.FromHtml("#A5D7E8");
            buttonSignUp.BackColor = ColorTranslator.FromHtml("#05982E");

            // Wire up the linkLabelSignUp's Click event to show the signup panel
            linkLabelSignInToSignUp.Click += LinkLabelSignUp_Click;
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
                errorProvider.SetError(textBoxSignInUserName, "");
                errorProvider.SetError(textBoxSignInPassword, "");
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
                    Password = textBoxPasswrd.Text,
                    UserName = $"{textBoxFirstname.Text} {textBoxLastname.Text}"
                };

                _presenter.SaveUserRegistration(userCredentials); // Use presenter to call the save method
                MessageBox.Show("Registration successful."); // Show a success message
                panelSignUp.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private async void button_SignIn_Click(object sender, EventArgs e)
        {
            try
            {
                button_SignInButton.Enabled = false; // Disable the signin button

                var userCredentials = new UserCredentialsDto
                {
                    UserName = textBoxSignInUserName.Text,
                    Password = textBoxSignInPassword.Text
                };

                bool isValidCredentials = await _presenter.ValidateUserCredentials(userCredentials); // Use presenter to call the validate method


                if (isValidCredentials)
                {
                    this.Close(); // Close the login form
                }
                else
                {
                    MessageBox.Show("Invalid credentials. Please try again.");
                }
            }
            catch (Exception ex)
            {
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
                if (string.IsNullOrWhiteSpace(textBoxSignInUserName.Text))
                {
                    errorProvider.SetError(textBoxSignInUserName, "Email Address is required.");
                    isValid = false;
                }
                if (string.IsNullOrWhiteSpace(textBoxSignInPassword.Text))
                {
                    errorProvider.SetError(textBoxSignInPassword, "Password is required.");
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
            button_SignInButton.Enabled = isValid;
        }

        private void buttonBackToSignIn_Click(object sender, EventArgs e)
        {
            // Hide the signup panel
            panelSignUp.Visible = false;
            panelSignIn.Visible = true;

        }


    }



}
