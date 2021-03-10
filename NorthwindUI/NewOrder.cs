using NorthwindUI.DataLayerModel;
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
    public partial class Admin : Form
    {
        DataTable Products;
        private int _updateOrderId = -1;
        private Form1 _theMainForm;


        public Admin()
        {
            InitializeComponent();
            Products = new DataTable();
            this.Text = "New Order";
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
            //this.productListTableAdapter.Fill(this.northwindDataSet11.ProductList);
            // TODO: This line of code loads data into the 'northwindDataSet10.CustomerList' table. You can move, or remove it, as needed.
            //this.customerListTableAdapter1.Fill(this.northwindDataSet10.CustomerList);
            // TODO: This line of code loads data into the 'northwindDataSet9.CustomerList' table. You can move, or remove it, as needed.
            //this.customerListTableAdapter.Fill(this.northwindDataSet9.CustomerList);

            lblDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

            cboCustomer.DataSource = DataLayerAPI.GetCustomers();//.Select(x => x.CompanyName).ToList();
            cboProductSelection.DataSource = DataLayerAPI.GetProducts();//.Select(x => x.productName).ToList();

            if (_updateOrderId > -1)
            {
                this.Text = "Update Order";
                cboCustomer.Enabled = false;

                //populate details and products
                //DataLayer dbMethods = new DataLayer();

                MainOrderDetails orderDetail = DataLayerAPI.OrderDetails(_updateOrderId);
                
                cboCustomer.Text = orderDetail.CompanyName.ToString();

                //CustOrdersDetail productsInOrder = DataLayerAPI.ProductsInOrder(_updateOrderId);

                

                this.dgvProducts.DataSource = DataLayerAPI.ProductsInOrder(_updateOrderId);

                lblOrderNumber.Text = _updateOrderId.ToString();

                //foreach (DataRow row in productsInOrder.Rows)
                //{
                //    this.dgvProducts.Rows.Add(row["ProductID"], row["ProductName"], row["UnitPrice"], "Existing");
                //    //add product to dgvProducts
                //}
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboProductSelection.Text == "")
            {
                MessageBox.Show("Please select a product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // DataLayer dbMethods = new DataLayer();

            Product productDetails = DataLayerAPI.GetProductDetails((int)cboProductSelection.SelectedValue);

            this.dgvProducts.Rows.Add(productDetails.ProductId, productDetails.ProductName, productDetails.UnitPrice, "New");//, productDetails[3]);

            string selectedProduct = cboCustomer.Text;
            cboCustomer.DataSource = null;
            cboCustomer.Text = selectedProduct;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //DataLayer dbMethods = new DataLayer();

            if (_updateOrderId == -1)
            {
                CreateOrder();
            }
            else
            {
                //update order
                UpdateOrder();
            }
            _theMainForm.RefreshOrders();
        }

        private void UpdateOrder()
        {
            //add new products to order & ignore existing orders
            //InitializeComponent();
           //this.Text = "Update Order";
           
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells[3].Value.ToString() == "New")
                {
                    DataLayer.AddProductToOrder(_updateOrderId, (int)row.Cells[0].Value, (decimal)row.Cells[2].Value);
                }
            }
        }

        private void CreateOrder()
        {
            int orderId = DataLayer.CreateNewOrder(cboCustomer.Text, 1, DateTime.Parse(lblDate.Text), DateTime.Now, DateTime.Now.AddDays(5));

            lblOrder.Text = orderId.ToString();

            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                DataLayer.AddProductToOrder(orderId, (int)row.Cells[0].Value, Convert.ToDecimal(row.Cells[2].Value));
            }
            //add products to order
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dgvProducts.SelectedRows[0];

            var confirmResult = MessageBox.Show("Are you sure to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                //get selected productId
                //DataLayer dbMethods = new DataLayer();

                int productId = (int)selectedRow.Cells[0].Value;

                //excute sp to delete product
                DataLayer.DeleteProductsFromOrder(productId, _updateOrderId);

                //clears rows
                dgvProducts.DataSource = null;

                dgvProducts.Rows.Clear();

                //refreshes product list
                //DataTable productsInOrder = DataLayer.ProductsInOrder(_updateOrderId);

                this.dgvProducts.DataSource = DataLayerAPI.ProductsInOrder(_updateOrderId);

                //foreach (DataRow row in productsInOrder.Rows)
                //{
                //    this.dgvProducts.Rows.Add(row["ProductID"], row["ProductName"], row["UnitPrice"], "Existing");
                //    //add product to dgvProducts
                //}
            }
            //refreshes main form
            _theMainForm.RefreshOrders();
        }
    }
}
