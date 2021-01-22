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

        private void button1_Click(object sender, EventArgs e)
        {
            AllOrders.Text = DateTime.Now.ToString();
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DBMethods dbMethods = new DBMethods();
            int orderId = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            DBMethods.OrderDetailType orderDetail = dbMethods.OrderDetails(orderId);
            
            
            lblOrderNumber.Text = orderId.ToString();
            lblOrderDate.Text = orderDetail.OrderDate.ToString("dd/MM/yyyy");
            lblShippedDate.Text = orderDetail.ShippedDate.ToString("dd/MM/yyyy");
            lblCompanyName.Text = orderDetail.CompanyName.ToString();
            lblContactName.Text = orderDetail.ContactName.ToString();
            //test

            //custOrderHistBindingSource.ResetBindings.
            //lblProductName.Text = orderDetail.ProductName;

            dgvProducts.DataSource = dbMethods.OrderDetailReader(orderId);
            

            //if ()
            //this.custOrdersDetailTableAdapter.Fill(this.northwindDataSet5.CustOrdersDetail, (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            //this.custOrdersDetailTableAdapter.Fill(this.northwindDataSet5.CustOrdersDetail, (int) dataGridView1.SelectedRows[0].Cells[0].Value);

            //Application.DoEvents();

            //this.northwindDataSet5.CustOrdersDetail.Rows[0].ItemArray[0].ToString();
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
            newOrder.Show();
        }
    }
}
