using KeyvaultWindowsFormsApp.Models;
using KeyvaultWindowsFormsApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyvaultWindowsFormsApp
{
    public partial class Form1 : Form
    {
        CallWebApi callWebApi = new CallWebApi();
        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            LoginViewModel model = new LoginViewModel();
            
            model.Email = textBox1.Text;
            model.Password = maskedTextBox1.Text;
            var postTask =  callWebApi.Login(model);
            if (postTask)
            {
                this.Hide();
                Form2 f2 = new Form2();
                f2.Show();
            }
            else
            {
                MessageBox.Show("username or password incorrect");
            }
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm rf = new RegisterForm();
            rf.Show();
        }
    }
}
