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
        public ManageProducts()
        {
            InitializeComponent();
            this.Text = "Update Products";
        }

        public void SetMainForm(Form1 theMainForm)
        {
          //  _theMainForm = theMainForm;
        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'northwindDataSet12.ProductList' table. You can move, or remove it, as needed.
            this.productListTableAdapter.Fill(this.northwindDataSet12.ProductList);
        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {

        }

        private void bntProductUpdate_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in dgvProductList.Rows)
           // {
            //    if (row.Cells[3].Value.ToString() == "New")
             //   {
             //       dbMethods.AddProductToList(_updateProductId, (int)row.Cells[0].Value, (decimal)row.Cells[2].Value);
              //  }
            //}
        }

        private void bntProductDelete_Click(object sender, EventArgs e)
        {
           // DataGridViewRow selectedRow = dgvProductList.SelectedRows[0];

            //var confirmResult = MessageBox.Show("Are you sure to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo);

           // if (confirmResult == DialogResult.Yes)
          //  {
           //     DBMethods dbMethods = new DBMethods();
           //     int productId = (int)selectedRow.Cells[0].Value;

                //excute sp to delete product
           //     dbMethods.DeleteProductsFromOrder(productId, _updateProductId);

                //clears rows
           //     dgvProductList.DataSource = null;

            //    dgvProductList.Rows.Clear();

                //refreshes product list
            //    DataTable productsInOrder = dbMethods.ProductsInOrder(_updateOrderId);

           //     foreach (DataRow row in productsInOrder.Rows)
            //    {
            //        this.dgvProductList.Rows.Add(row["ProductID"], row["ProductName"], row["UnitPrice"], "Existing");
                    //add product to dgvProducts
            //    }
          //  }
        }

        private void btnProductRefresh_Click(object sender, EventArgs e)
        {

        }

        private void dgvProductList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DBMethods dbMethods = new DBMethods();
            int orderId = (int)dgvProductList.Rows[e.RowIndex].Cells[0].Value;
            DBMethods.OrderDetailType orderDetail = dbMethods.OrderDetails(orderId);


            lblOrderNumber.Text = orderId.ToString();
            lblOrderDate.Text = orderDetail.OrderDate.ToString("MM/dd/yyyy");
            lblShippedDate.Text = orderDetail.ShippedDate.ToString("MM/dd/yyyy");
            lblCompanyName.Text = orderDetail.CompanyName.ToString();
            lblContactName.Text = orderDetail.ContactName.ToString();

            dgvProducts.DataSource = dbMethods.ProductsInOrder(orderId);
        }
    }
}
