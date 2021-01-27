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
        private int _updateOrderId = -1;
        private Form1 _theMainForm;


        public NewOrder()
        {
            InitializeComponent();
            Products = new DataTable();
        }

        public void UpdateOrder(int orderId)
        {
            _updateOrderId = orderId;
        }

        public void SetMainForm(Form1 theMainForm)
        {
            _theMainForm = theMainForm;
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
            if (_updateOrderId > -1)
            {
                cboCustomer.Enabled = false;

                //populate details and products
                DBMethods dbMethods = new DBMethods();

                DBMethods.OrderDetailType orderDetail = dbMethods.OrderDetails(_updateOrderId);
                cboCustomer.Text = orderDetail.CompanyName.ToString();

                //cboProductSelection.Text = orderDetail.ProductName.ToString();
                //test

                //custOrderHistBindingSource.ResetBindings.
                //lblProductName.Text = orderDetail.ProductName;

                //dgvProducts.DataSource = dbMethods.OrderDetailReader(_updateOrderId);
                DataTable productsInOrder = dbMethods.ProductsInOrder(_updateOrderId);

                //DataTable tempDT = new DataTable();
                //tempDT = productsInOrder.DefaultView.ToTable(true, "ProductID", "ProductName", "ExtendedPrice");
                //dgvProducts.DataSource = tempDT;

                lblOrderNumber.Text = _updateOrderId.ToString();

                foreach (DataRow row in productsInOrder.Rows)
                {
                    this.dgvProducts.Rows.Add(row["ProductID"], row["ProductName"], row["UnitPrice"]);
                    //add product to dgvProducts
                }


            }
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
            if (_updateOrderId == -1)
            {
                CreateOrder(dbMethods);
            }
            else
            {
                //update order
                UpdateOrder(dbMethods);
            }
            _theMainForm.RefreshOrders();
        }

        private void UpdateOrder(DBMethods dbMethods)
        {
            throw new NotImplementedException();
        }

        private void CreateOrder(DBMethods dbMethods)
        {
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
