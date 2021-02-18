using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NorthwindUI
{
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
            this.Text = "Admin";
        }

        public void SetMainForm(Form1 theMainForm)
        {
            //  _theMainForm = theMainForm;
        }

        private void AdminPage_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet15.Users' table. You can move, or remove it, as needed.
            //this.usersTableAdapter.Fill(this.northwindDataSet15.Users);

        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            //shows textboxes
            gboxNewUser.Visible = true;
        }

        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            DataLayer.AddUser(txtUsername.Text, txtPassword.Text);

            //refreshes List
            //this.usersTableAdapter.Fill(this.northwindDataSet15.Users);

            //hides textboxes + clears textboxes
            gboxNewUser.Visible = false;
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dgvUsers.SelectedRows[0];

            var confirmResult = MessageBox.Show("Are you sure to delete this user?", "Confirm", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                string UserId = selectedRow.Cells[0].Value.ToString();

                //excute sp to delete product
                DataLayer.DeleteUser(UserId);

                //clears rows
                //dgvUsers.DataSource = null;

                //dgvUsers.Rows.Clear();

                //refreshes list
                //this.usersTableAdapter.Fill(this.northwindDataSet15.Users);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //this.usersTableAdapter.Fill(this.northwindDataSet15.Users);
        }
    }
}
