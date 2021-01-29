using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindUI
{
    class DBMethods
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
        public OrderDetailType OrderDetails(int OrderId)
        {

            using (SqlConnection conn = new SqlConnection("Server=192.168.1.110;DataBase=Northwind;User Id=Sierra;Password=detail;"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("CustOrdersDetail", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderId));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    OrderDetailType orderDetail = new OrderDetailType();
                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        orderDetail.ProductName = rdr["ProductName"].ToString();
                        orderDetail.UnitPrice = Double.Parse(rdr["UnitPrice"].ToString());// rdr.GetDouble(1);
                        orderDetail.Quantity = int.Parse(rdr["Quantity"].ToString());// rdr.GetDouble(1);
                        orderDetail.Discount = Double.Parse(rdr["Discount"].ToString());// rdr.GetDouble(1);
                        orderDetail.ExtendedPrice = Double.Parse(rdr["ExtendedPrice"].ToString());// rdr.GetDouble(1);
                        orderDetail.OrderDate = DateTime.Parse(rdr["OrderDate"].ToString());// rdr.GetDouble(1);
                        orderDetail.ShippedDate = DateTime.Parse(rdr["ShippedDate"].ToString());
                        orderDetail.CompanyName = rdr["CompanyName"].ToString();
                        orderDetail.ContactName = rdr["ContactName"].ToString();
                        //Console.WriteLine("Product: {0,-35} Total: {1,2}", rdr["ProductName"], rdr["Total"]);
                    }
                    return orderDetail;
                }
            }
        }

        public DataTable ProductsInOrder(int OrderId)
        {

            using (SqlConnection conn = new SqlConnection("Server=192.168.1.110;DataBase=Northwind;User Id=Sierra;Password=detail;"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("CustOrdersDetail", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderId));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(rdr);
                    return dt;
                }
            }
        }

        public DataRow GetProductDetails(int ProductId)
        {

            using (SqlConnection conn = new SqlConnection("Server=192.168.1.110;DataBase=Northwind;User Id=Sierra;Password=detail;"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("GetProductDetail", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductId));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(rdr);
                    return dt.Rows[0];
                }
            }
        }

        public int CreateNewOrder(String CustomerName, int EmployeeID, DateTime OrderDate, DateTime RequiredDate, DateTime ShippedDate)
        {

            using (SqlConnection conn = new SqlConnection("Server=192.168.1.110;DataBase=Northwind;User Id=Sierra;Password=detail;"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("AddNewOrder", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@CustomerName", CustomerName));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", EmployeeID));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", OrderDate));
                cmd.Parameters.Add(new SqlParameter("@RequiredDate", RequiredDate));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", ShippedDate));

                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    return (int)rdr.GetDecimal(0);
                    //return rdr.GetInt32(0);
                }
            }
        }

        public void AddProductToOrder(int OrderID, int ProductID, decimal UnitPrice)//, int Quantity)//, int Discount)
        {

            using (SqlConnection conn = new SqlConnection("Server=192.168.1.110;DataBase=Northwind;User Id=Sierra;Password=detail;"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("AddProductsToOrder", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@UnitPrice", UnitPrice));
                cmd.Parameters.Add(new SqlParameter("@Quantity", 1));
                cmd.Parameters.Add(new SqlParameter("@Discount", "0"));

                // execute the command
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteOrder(int OrderID)
        {

            using (SqlConnection conn = new SqlConnection("Server=192.168.1.110;DataBase=Northwind;User Id=Sierra;Password=detail;"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("DeleteOrder", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));

                // execute the command
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProductsFromOrder(int ProductID, int OrderID)
        {

            using (SqlConnection conn = new SqlConnection("Server=192.168.1.110;DataBase=Northwind;User Id=Sierra;Password=detail;"))
            {
                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("DeleteProductsFromOrder", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                cmd.Parameters.Add(new SqlParameter("@OrderID", OrderID));

                // execute the command
                cmd.ExecuteNonQuery();
            }
        }

    }
}
