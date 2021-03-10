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
    public partial class ManageProducts : Form
    {
        private int _updateProductId = -1;

        public ManageProducts()
        {
            InitializeComponent();
            this.Text = "Manage Products";
        }

        //public void UpdateProduct(int productId)
        //{
        //    _updateProductId = productId;
        //}

        public void SetMainForm(Form1 theMainForm)
        {
          //  _theMainForm = theMainForm;
        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet12.ProductList' table. You can move, or remove it, as needed.
            //gthis.productListTableAdapter.Fill(this.northwindDataSet12.ProductList);
            //dgvProductList.DataSource = DataLayerAPI.AllProducts();
            dgvProductList.DataSource = DataLayerAPI.GetProducts();

            if (_updateProductId > -1)
            {
                this.dgvProductList.DataSource = DataLayerAPI.ProductsInOrder(_updateProductId);
            }
        }

        private void dgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int productId = (int)dgvProductList.Rows[e.RowIndex].Cells[0].Value;
            Product productDetail = DataLayerAPI.GetProductDetails(productId);

            lblProductName.Text = productDetail.ProductName;
            lblUnitPrice.Text = productDetail.UnitPrice.ToString();

            if (productDetail.Discontinued == true)
            {
                lblDiscontinued.Text = "Yes";
            }
            else
            {
                lblDiscontinued.Text = "No";
            }

            _updateProductId = productId;

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

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            Product product = new Product();

            product.ProductName = txtProductName.Text;
            product.QuantityPerUnit = txtQuantity.Text;
            product.UnitPrice = int.Parse(txtPrice.Text);
            product.UnitsInStock = short.Parse(txtUnitsInStock.Text);
            product.CategoryId = 2;
            product.SupplierId = 29;
            //product.Supplier = GetDefaultSupplier();

            if (_updateProductId == -1)
            {
                DataLayerAPI.AddProduct(product);
            }
            else
            {
                product.ProductId = _updateProductId;
                //update Product
                DataLayerAPI.UpdateProduct(product);
                lblDetails.Visible = false;
                lblUpdate.Visible = false;
                lblCreateNewProduct.Visible = false;
                txtProductName.Visible = false;
                lblProductName.Visible = true;
                txtQuantity.Visible = false;
                txtUnitsInStock.Visible = false;
                txtPrice.Visible = false;
                btnSaveProduct.Visible = false;
                btnCancel.Visible = false;
            }


            //refreshes List
            dgvProductList.DataSource = DataLayerAPI.GetProducts();

            //clears textboxes
            txtProductName.Clear();
            txtQuantity.Clear();
            txtUnitsInStock.Clear();
            txtPrice.Clear();
        }

        //private Supplier GetDefaultSupplier()
        //{
        //    Supplier supplier = new Supplier();
        //    supplier.SupplierId = 29;
        //    supplier.Address = "148 rue Chasseur";
        //    supplier.City = "";
        //    supplier.CompanyName = "";
        //    supplier.ContactName = "";
        //    supplier.ContactTitle = "";
        //    supplier.Country = "";
        //    supplier.Fax = "";
        //    supplier.HomePage = "";
        //    supplier.Phone = "";
        //    supplier.PostalCode = "";
        //    supplier.Products = ;

        //    return supplier;
        //}

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
                dgvProductList.DataSource = DataLayerAPI.GetProducts();
            }
        }

        private void btnProductRefresh_Click(object sender, EventArgs e)
        {
            //Refreshes product List
            dgvProductList.DataSource = DataLayerAPI.GetProducts();
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
                dgvProductList.DataSource = DataLayerAPI.GetProducts();
            }
        }
    }
}
