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
    public partial class UserAndPassword : Form
    {
        private Form1 _theMainForm;

        public UserAndPassword()
        {
            InitializeComponent();
        }

        public void SetMainForm(Form1 theMainForm)
        {
             _theMainForm = theMainForm;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string UserId = txtUserId.Text;
            string Password = txtPassword.Text;

            if (DataLayer.IDConfirmation(UserId, Password) == true)
            {
                //switch to main form + close this form
                new Form1().Show();
                this.Hide();
            }
            //if not, ask if they want to try again
            else if (DataLayer.IDConfirmation(UserId, Password) != true)
            {
                var confirmResult = MessageBox.Show("Try again?", "Confirm", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    //tries again
                    this.Show();
                }
                else
                {
                    //end program
                    Application.Exit();
                }
            }
        }

        private void UserAndPassword_Load(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }
    }
}
