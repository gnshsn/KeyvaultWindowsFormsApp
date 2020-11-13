using KeyvaultWindowsFormsApp.Models;
using KeyvaultWindowsFormsApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KeyvaultWindowsFormsApp
{
    public partial class RegisterForm : Form
    {
        CallWebApi callWebApi = new CallWebApi();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, EventArgs e)
        {
            RegisterBindingModel model = new RegisterBindingModel();
            model.Email = Username.Text;
            model.Password = Password.Text;
            model.ConfirmPassword = PasswordCon.Text;
            var postTask = callWebApi.Register(model);
            if (postTask)
            {
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please try again");
            }
        }
    }
}
