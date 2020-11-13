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
    public partial class ShareForm : Form
    {
        CallWebApi callWebApi = new CallWebApi();
        string keyID = null;
        KeyViewModel model = new KeyViewModel();
        public ShareForm(KeyViewModel key)
        {
            InitializeComponent();
            model.Id = key.Id;
            model.Username = key.Username;
            model.UserId = key.UserId;
            model.Password = key.Password;
            model.ExpirationDate = key.ExpirationDate;
            model.CreateDate = key.CreateDate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            model.UserId = comboBox1.SelectedValue.ToString();
            if (callWebApi.CreateKey(model))
            {
                Form2 f2 = new Form2();
                f2.LoadRows();
                f2.CheckExpirationDate();
                this.Close();
            }

        }

        private void ShareForm_Load(object sender, EventArgs e)
        {
            IEnumerable<UserListViewModel> users = callWebApi.GetUsers();
            comboBox1.DataSource = users;
            comboBox1.DisplayMember = "UserName";
            comboBox1.ValueMember = "UserId";
        }
    }
}
