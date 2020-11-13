using KeyvaultWindowsFormsApp.Models;
using KeyvaultWindowsFormsApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Windows.Forms;

namespace KeyvaultWindowsFormsApp
{
    public partial class Form3 : Form
    {
        string keyID = null;
        bool update = false;
        CallWebApi callWebApi = new CallWebApi();
        KeyViewModel key = new KeyViewModel();
        public Form3(string id)
        {
            InitializeComponent();
            keyID = id;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if (keyID != null)
            {
                update = true;
                key = callWebApi.GetKey(keyID);
                username.Text = key.Username;
                password.Text = key.Password;
                key.ExpirationDate = DateTime.ParseExact(key.ExpirationDate.ToString("dd/mm/yyyy"), "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None);
                expDate.Value = key.ExpirationDate;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            if (update)
            {
                key.Username = username.Text;
                key.Password = password.Text;
                key.ExpirationDate = expDate.Value;
                if (callWebApi.UpdateKey(key))
                {
                    key = null;
                    update = false;
                    f2.LoadRows();
                    f2.CheckExpirationDate();
                    this.Close();
                }
            }
            else
            {
                key.Username = username.Text;
                key.Password = password.Text;
                key.ExpirationDate = expDate.Value;
                if (callWebApi.CreateKey(key))
                {
                    key = null;
                    update = false;
                    f2.LoadRows();
                    f2.CheckExpirationDate();
                    this.Close();
                    
                }
            }
        }
    }
}
