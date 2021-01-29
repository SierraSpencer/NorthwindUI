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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Main Screen";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet4.AllOrders' table. You can move, or remove it, as needed.
            this.allOrdersTableAdapter2.Fill(this.northwindDataSet4.AllOrders);
            // TODO: This line of code loads data into the 'northwindDataSet3.AllOrders' table. You can move, or remove it, as needed.
            //this.allOrdersTableAdapter1.Fill(this.northwindDataSet3.AllOrders);
            // TODO: This line of code loads data into the 'northwindDataSet1.AllOrders' table. You can move, or remove it, as needed.
            //this.allOrdersTableAdapter.Fill(this.northwindDataSet1.AllOrders);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DBMethods dbMethods = new DBMethods();
            int orderId = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            DBMethods.OrderDetailType orderDetail = dbMethods.OrderDetails(orderId);
            
            
            lblOrderNumber.Text = orderId.ToString();
            lblOrderDate.Text = orderDetail.OrderDate.ToString("MM/dd/yyyy");
            lblShippedDate.Text = orderDetail.ShippedDate.ToString("MM/dd/yyyy");
            lblCompanyName.Text = orderDetail.CompanyName.ToString();
            lblContactName.Text = orderDetail.ContactName.ToString();

            dgvProducts.DataSource = dbMethods.ProductsInOrder(orderId); 
        }

        private void fillToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.custOrdersDetailTableAdapter.Fill(this.northwindDataSet5.CustOrdersDetail, new System.Nullable<int>(((int)(System.Convert.ChangeType(orderIDToolStripTextBox.Text, typeof(int))))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lblContactName_Click(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            NewOrder newOrder = new NewOrder();
            newOrder.SetMainForm(this);
            newOrder.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            if (!int.TryParse(selectedRow.Cells[0].Value.ToString(), out _))
            {
                MessageBox.Show("This order does not have an ID", "Error", MessageBoxButtons.OK);
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure to delete this Order?", "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                // If 'Yes', do something here.
                //get selected orderId
                DBMethods dbMethods = new DBMethods();
                int orderId = (int)selectedRow.Cells[0].Value;

                //excute sp to delete order
                dbMethods.DeleteOrder(orderId);

                //refresh grid
                this.allOrdersTableAdapter2.Fill(this.northwindDataSet4.AllOrders);

                //clears rows
                //dgvProducts.Rows.Clear();
                dgvProducts.DataSource = null;
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            //update
            NewOrder newOrder = new NewOrder();

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int orderId = (int)selectedRow.Cells[0].Value;

            newOrder.UpdateOrder(orderId);
            newOrder.SetMainForm(this);
            //newOrder.TopMost = true;
            //newOrder.Show(this);
            newOrder.Show();

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.allOrdersTableAdapter2.Fill(this.northwindDataSet4.AllOrders);
        }

        public void RefreshOrders()
        {
            this.allOrdersTableAdapter2.Fill(this.northwindDataSet4.AllOrders);
        }

        private void btnProductListForm_Click(object sender, EventArgs e)
        {
            ManageProducts manageProducts = new ManageProducts();
            manageProducts.SetMainForm(this);
            manageProducts.Show();
        }
    }
}
