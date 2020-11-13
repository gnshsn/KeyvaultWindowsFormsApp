using KeyvaultWindowsFormsApp.Models;
using KeyvaultWindowsFormsApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace KeyvaultWindowsFormsApp
{
    public partial class Form2 : Form
    {
        CallWebApi callWebApi = new CallWebApi();
        public Form2()
        {
            InitializeComponent();
        }

        public void LoadRows()
        {
            try
            {
                IEnumerable<KeyViewModel> keys = callWebApi.GetKeys(StaticGlobalVariables.UserID);
                dataGridView1.DataSource = null;
                foreach (var item in keys)
                {
                    dataGridView1.Rows.Add(item.Id, item.UserId, item.Username, item.Password, item.ExpirationDate, item.CreateDate);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void CheckExpirationDate()
        {
            DateTime dt = new DateTime();
            int i = 0;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells[4].Value != null)
                {
                    string date = item.Cells[4].Value.ToString();
                    dt = DateTime.Parse(date);
                    dt = DateTime.ParseExact(dt.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if ((dt - DateTime.Now).TotalDays <= 0)
                    {
                        DataGridViewRow row = dataGridView1.Rows[i];
                        row.Cells[4].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }
                    else if ((dt - DateTime.Now.AddDays(7)).TotalDays <= 0)
                    {
                        DataGridViewRow row = dataGridView1.Rows[i];
                        row.Cells[4].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
                    }
                    i++;
                }
            }
        }
        public void CreateComboboxList()
        {
            ComboboxItem combo = new ComboboxItem();
            combo.Text = "Username";
            combo.Value = "username";
            comboBox1.Items.Add(combo);
            combo = new ComboboxItem();
            combo.Text = "Expiration Date";
            combo.Value = "expdate";
            comboBox1.Items.Add(combo);
            combo = new ComboboxItem();
            combo.Text = "Create Date";
            combo.Value = "crtdate";
            comboBox1.Items.Add(combo);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                LoadRows();
                CheckExpirationDate();
                CreateComboboxList();
                UserLogViewModel uLogin = new UserLogViewModel();
                uLogin = callWebApi.GetLastLogin();
                loginInfo.Text = "Last Login ----> " + uLogin.LoginTime.ToString();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = null;
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns["Edit"].Index == e.ColumnIndex)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();
                Form3 f3 = new Form3(id);
                f3.TopMost = true;
                f3.Show();
            }
            else if (senderGrid.Columns["Share"].Index == e.ColumnIndex)
            {
                KeyViewModel key = new KeyViewModel();
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                key.Id = row.Cells[0].Value.ToString();
                key.UserId = row.Cells[1].Value.ToString();
                key.Username = row.Cells[2].Value.ToString();
                key.Password = row.Cells[3].Value.ToString();
                key.ExpirationDate = DateTime.Parse(row.Cells[4].Value.ToString(), CultureInfo.InvariantCulture,
                                DateTimeStyles.None);
                ShareForm sh = new ShareForm(key);
                sh.TopMost = true;
                sh.Show();
            }
            else if(senderGrid.Columns["Delete"].Index == e.ColumnIndex) {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                id = row.Cells[0].Value.ToString();
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;

                // Displays the MessageBox.
                result = MessageBox.Show("Are u sure, would you like to delete this data?", "Delete Warning", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (callWebApi.DeleteRow(id))
                    {
                        MessageBox.Show("Data succesfully deleted.");
                        dataGridView1.Update();
                    }
                }
            }
            else if (senderGrid.Columns["Username"].Index == e.ColumnIndex )
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Clipboard.SetText(row.Cells[2].Value.ToString());
                MessageBox.Show("Username coppied!");
            }
            else if (senderGrid.Columns["Password"].Index == e.ColumnIndex)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Clipboard.SetText(row.Cells[3].Value.ToString());
                MessageBox.Show("Password coppied!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = null;
            Form3 f3 = new Form3(a);
            f3.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
