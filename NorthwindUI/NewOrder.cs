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
    public partial class NewOrder : Form
    {
        DataTable Products;

        public NewOrder()
        {
            InitializeComponent();
            Products = new DataTable();
        }

        private void NewOrder_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet11.ProductList' table. You can move, or remove it, as needed.
            this.productListTableAdapter.Fill(this.northwindDataSet11.ProductList);
            // TODO: This line of code loads data into the 'northwindDataSet10.CustomerList' table. You can move, or remove it, as needed.
            this.customerListTableAdapter1.Fill(this.northwindDataSet10.CustomerList);
            // TODO: This line of code loads data into the 'northwindDataSet9.CustomerList' table. You can move, or remove it, as needed.
            this.customerListTableAdapter.Fill(this.northwindDataSet9.CustomerList);
            lblDate.Text = DateTime.Now.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboProductSelection.Text == "")
            {
                MessageBox.Show("Please select a product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DBMethods dbMethods = new DBMethods();

            DataRow productDetails = dbMethods.GetProductDetails((int)cboProductSelection.SelectedValue);

            this.dgvProducts.Rows.Add(productDetails[0], productDetails[1], productDetails[2]);//, productDetails[3]);
           // DataGridViewRow row = (DataGridViewRow)dgvProducts.Rows[0].Clone();

            //for (int i = 0; i < 3; i++)
            //{
            //    row.Cells[i].Value = productDetails[i];
            //}

            //dgvProducts.Rows.Add(row);

            string selectedPruduct = cboCustomer.Text;
            cboCustomer.DataSource = null;
            cboCustomer.Text = selectedPruduct;
            

            // Products.Rows.Add(productDetails);
            //Products.ImportRow(productDetails);
            //dgvProducts.DataSource = Products;
            //addproducttogrid


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DBMethods dbMethods = new DBMethods();

            int orderId = dbMethods.CreateNewOrder(cboCustomer.Text, 1, DateTime.Parse(lblDate.Text), DateTime.Now, DateTime.Now.AddDays(5));

            lblOrder.Text = orderId.ToString();

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                dbMethods.AddProductToOrder(orderId, (int)row.Cells[0].Value, (decimal)row.Cells[2].Value);
            }
            //add products to order
        }
    }
}
