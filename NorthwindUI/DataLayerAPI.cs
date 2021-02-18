using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindUI
{
    class DataLayerAPI
    {
        public struct OrderDetailType
        {
            public string ProductName;
            public double UnitPrice;
            public int Quantity;
            public double Discount;
            public double ExtendedPrice;
            public DateTime OrderDate;
            public DateTime ShippedDate;
            public string CompanyName;
            public string ContactName;

        }

        public struct ProductDetailType
        {
            public string ProductName;
            public double UnitPrice;
            public string Discontinued;
            public int UnitsOnOrder;
            public int UnitsInStock;
            public double QuantityPerUnit;
            public int ReorderLevel;
        }

        //public static ProductDetailType ProductDetails(int ProductId)
        //{

            //using (SqlConnection conn = new SqlConnection(_connectionString))
            //{
            //    conn.Open();

            //    // 1.  create a command object identifying the stored procedure
            //    SqlCommand cmd = new SqlCommand("AllProductDetails", conn);

            //    // 2. set the command object so it knows to execute a stored procedure
            //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

            //    // 3. add parameter to command, which will be passed to the stored procedure
            //    cmd.Parameters.Add(new SqlParameter("@ProductID", ProductId));

            //    // execute the command
            //    using (SqlDataReader rdr = cmd.ExecuteReader())
                //{
                //    ProductDetailType productDetail = new ProductDetailType();
                //    // iterate through results, printing each to console
                //    while (rdr.Read())
                //    {
                //        productDetail.ProductName = rdr["ProductName"].ToString();
                //        productDetail.UnitPrice = Double.Parse(rdr["UnitPrice"].ToString());// rdr.GetDouble(1);
                //        productDetail.Discontinued = rdr["Discontinued"].ToString();// rdr.GetDouble(1);
                //        productDetail.UnitsOnOrder = int.Parse(rdr["UnitsOnOrder"].ToString());// rdr.GetDouble(1);
                //        productDetail.UnitsInStock = int.Parse(rdr["UnitsInStock"].ToString());// rdr.GetDouble(1);
                //        productDetail.QuantityPerUnit = Double.Parse(rdr["UnitPrice"].ToString());// rdr.GetDouble(1);
                //        productDetail.ReorderLevel = int.Parse(rdr["ReorderLevel"].ToString());
                //        //Console.WriteLine("Product: {0,-35} Total: {1,2}", rdr["ProductName"], rdr["Total"]);
                //    }
                //    return productDetail;
                //}
            //}
        //}
    }
}
