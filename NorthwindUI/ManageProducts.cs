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
    public partial class ManageProducts : Form
    {
        private int _updateProductId = -1;

        public ManageProducts()
        {
            InitializeComponent();
            this.Text = "Manage Products";
        }

        public void UpdateProduct(int productId)
        {
            _updateProductId = productId;
        }

        public void SetMainForm(Form1 theMainForm)
        {
          //  _theMainForm = theMainForm;
        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet12.ProductList' table. You can move, or remove it, as needed.
            //gthis.productListTableAdapter.Fill(this.northwindDataSet12.ProductList);
            dgvProductList.DataSource = DataLayer.AllProducts();

            if (_updateProductId > -1)
            {
                DataLayer.ProductDetailType productDetail = DataLayer.ProductDetails(_updateProductId);
               // cboCustomer.Text = productDetail.CompanyName.ToString();

                DataTable productsInOrder = DataLayer.ProductsInOrder(_updateProductId);

                //lblOrderNumber.Text = _updateProductId.ToString();

                foreach (DataRow row in productsInOrder.Rows)
                {
                    //int ProductID, string ProductName, string QuantityPerUnit, decimal UnitPrice, int UnitsInStock
                    this.dgvProductList.Rows.Add(row["ProductID"], row["ProductName"], row["Quantity Per Unit"], row["Price"], row["Units In Stock"], "Existing");
                    //add product to dgvProducts
                }
            }
        }

        private void dgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int productId = (int)dgvProductList.Rows[e.RowIndex].Cells[0].Value;
            DataLayer.ProductDetailType productDetail = DataLayer.ProductDetails(productId);

            lblProductName.Text = productDetail.ProductName.ToString();
            lblUnitPrice.Text = productDetail.UnitPrice.ToString();

            if (productDetail.Discontinued == "True")
            {
                lblDiscontinued.Text = "Yes";
            }
            else
            {
                lblDiscontinued.Text = "No";
            }

            lblUnitsOnOrder.Text = productDetail.UnitsOnOrder.ToString();
            lblUnitsInStock.Text = productDetail.UnitsInStock.ToString();
            lblQuantityPerUnit.Text = productDetail.QuantityPerUnit.ToString();
            lblReorderLevel.Text = productDetail.ReorderLevel.ToString();
        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            //show/hide textboxes
            lblDetails.Visible = false;
            lblCreateNewProduct.Visible = true;
            txtProductName.Visible = true;
            lblProductName.Visible = false;
            txtQuantity.Visible = true;
            txtUnitsInStock.Visible = true;
            txtPrice.Visible = true;
            btnSaveProduct.Visible = true;
            btnCancel.Visible = true;
        }

        private void bntProductUpdate_Click(object sender, EventArgs e)
        {
            //show/hide textboxes
            lblDetails.Visible = false;
            lblUpdate.Visible = true;
            txtProductName.Visible = true;
            lblProductName.Visible = false;
            txtQuantity.Visible = true;
            txtUnitsInStock.Visible = true;
            txtPrice.Visible = true;
            btnSaveProduct.Visible = true;
            btnCancel.Visible = true;

            //fill text boxes with already set data
            txtProductName.Text = lblProductName.Text;
            txtQuantity.Text = lblQuantityPerUnit.Text;
            txtUnitsInStock.Text = lblUnitsInStock.Text;
            txtPrice.Text = lblUnitPrice.Text;
        }

        private void UpdateProduct()
        {
            //add new products to order & ignore existing orders
            foreach (DataGridViewRow row in dgvProductList.Rows)
            {
                if (row.Cells[3].Value.ToString() == "New")
                {
                    DataLayer.AddProduct(_updateProductId, txtProductName.Text, txtQuantity.Text, int.Parse(txtPrice.Text), int.Parse(txtUnitsInStock.Text));
                }
            }
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            if (_updateProductId == -1)
            {
               // DataRow addDetails = DataLayer.AllProducts();
                //this.dgvProductList.Rows.Add(addDetails[0], addDetails[1], addDetails[2], addDetails[3], addDetails[4], "New");

                DataLayer.AddProduct(_updateProductId, txtProductName.Text, txtQuantity.Text, int.Parse(txtPrice.Text), int.Parse(txtUnitsInStock.Text));
            }
            else
            {
                //update Product
                UpdateProduct();
            }
            //refreshes List
            dgvProductList.DataSource = DataLayer.AllProducts();

            //clears textboxes
            txtProductName.Clear();
            txtQuantity.Clear();
            txtUnitsInStock.Clear();
            txtPrice.Clear();
        }

        private void bntProductDelete_Click(object sender, EventArgs e)
        {
           DataGridViewRow selectedRow = dgvProductList.SelectedRows[0];

           var confirmResult = MessageBox.Show("Are you sure to discontinue this product?", "Confirm", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
               int productId = (int)selectedRow.Cells[0].Value;

                //excute sp to delete product
                DataLayer.DeleteAProduct(productId);

                //clears rows
                dgvProductList.DataSource = null;

                dgvProductList.Rows.Clear();

                //refreshes product list
                dgvProductList.DataSource = DataLayer.AllProducts();
            }
        }

        private void btnProductRefresh_Click(object sender, EventArgs e)
        {
            //Refreshes product List
            dgvProductList.DataSource = DataLayer.AllProducts();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //hide/show labels + textboxes
            lblDetails.Visible = true;
            lblUpdate.Visible = false;
            lblCreateNewProduct.Visible = false;
            txtProductName.Visible = false;
            lblProductName.Visible = true;
            txtQuantity.Visible = false;
            txtUnitsInStock.Visible = false;
            txtPrice.Visible = false;
            btnSaveProduct.Visible = false;
            btnCancel.Visible = false;

            //clears textboxes
            txtProductName.Clear();
            txtQuantity.Clear();
            txtUnitsInStock.Clear();
            txtPrice.Clear();
        }

        private void lblDiscontinued_Click(object sender, EventArgs e)
        {

        }

        private void btnRecontinue_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dgvProductList.SelectedRows[0];

            var confirmResult = MessageBox.Show("Are you sure to recontinue this product?", "Confirm", MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                int productId = (int)selectedRow.Cells[0].Value;

                //excute sp to recontinue product
                DataLayer.ReAddAProduct(productId);

                //clears rows
                dgvProductList.DataSource = null;

                dgvProductList.Rows.Clear();

                //refreshes product list
                dgvProductList.DataSource = DataLayer.AllProducts();
            }
        }
    }
}
