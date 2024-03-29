﻿using Newtonsoft.Json;
using NorthwindUI.DataLayerModel;
//using NorthwindUI.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNet.WebApi.Client;


namespace NorthwindUI
{
    class DataLayerAPI
    {
        //public struct OrderDetailType
        //{
        //    public string ProductName;
        //    public double UnitPrice;
        //    public int Quantity;
        //    public double Discount;
        //    public double ExtendedPrice;
        //    public DateTime OrderDate;
        //    public DateTime ShippedDate;
        //    public string CompanyName;
        //    public string ContactName;

        //}

        //public struct ProductDetailType
        //{
        //    public string ProductName;
        //    public double UnitPrice;
        //    public string Discontinued;
        //    public int UnitsOnOrder;
        //    public int UnitsInStock;
        //    public double QuantityPerUnit;
        //    public int ReorderLevel;
        //}

        static HttpClient client = new HttpClient();

        public static List<Product> GetProducts()//string path)
        {
            List<Product> listResult = new List<Product>();
            SetClient();

            HttpResponseMessage response = client.GetAsync("https://localhost:44324/api/Products").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                listResult = JsonConvert.DeserializeObject<List<Product>>(result);
            }
            return listResult;
        }

        public static List<MainGridOrder> GetOrders()//string path)
        {
            List<MainGridOrder> listResult = new List<MainGridOrder>();
            SetClient();

            HttpResponseMessage response = client.GetAsync("https://localhost:44324/api/GetAllOrders").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                listResult = JsonConvert.DeserializeObject<List<MainGridOrder>>(result);
            }
            return listResult;
        }

        private static void SetClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static MainOrderDetails OrderDetails(int OrderId)
        {
            MainOrderDetails orderDetails = new MainOrderDetails();
            SetClient();

            HttpResponseMessage response = client.GetAsync("https://localhost:44324/api/OrderDetails/" + OrderId.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {

                string result = response.Content.ReadAsStringAsync().Result;
                orderDetails = JsonConvert.DeserializeObject<MainOrderDetails>(result);

            }
            return orderDetails;

        }

        public static List<CustOrdersDetail> ProductsInOrder(int ProductID)
        {

            List<CustOrdersDetail> orderDetails = new List<CustOrdersDetail>();
            SetClient();

            HttpResponseMessage response = client.GetAsync("https://localhost:44324/api/CustOrdersDetail/" + ProductID.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {

                string result = response.Content.ReadAsStringAsync().Result;
                orderDetails = JsonConvert.DeserializeObject<List<CustOrdersDetail>>(result);
                //listResult = JsonConvert.DeserializeObject<List<MainGridOrder>>(result);

            }
            return orderDetails;
        }

        public static Product GetProductDetails(int ProductId)
        {
            Product orderDetails = new Product();
            SetClient();

            HttpResponseMessage response = client.GetAsync("https://localhost:44324/api/Products/" + ProductId.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                orderDetails = JsonConvert.DeserializeObject<Product>(result);
            }
            return orderDetails;
        }

        public static List<Customer> GetCustomers()//string path)
        {
            List<Customer> listResult = new List<Customer>();
            SetClient();

            HttpResponseMessage response = client.GetAsync("https://localhost:44324/api/Customers").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                listResult = JsonConvert.DeserializeObject<List<Customer>>(result);
            }
            return listResult;
        }

        public static void DeleteOrder(int OrderID)
        {

            SetClient();

            HttpResponseMessage response = client.DeleteAsync("https://localhost:44324/api/Orders/" + OrderID.ToString()).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error while trying to delete order " + response.Content);
            }
        }

        public async static void AddProduct(Product product)
        {
            SetClient();

            //StringContent sContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            //HttpResponseMessage response = client.PostAsync("https://localhost:44324/api/Products", sContent).Result;

            HttpResponseMessage response = await client.PostAsJsonAsync("api/products", product);
            //HttpContent content = new StringContent(product.ToString(), Encoding.UTF8, "application/json");
            //string content = JsonConvert.SerializeObject(product);

            //HttpResponseMessage response = client.PostAsJsonAsync("https://localhost:44324/api/Products", content).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error while trying to add a product " + response.Content);
            }
        }

        public static void UpdateProduct(Product product)
        {
            SetClient();

            //HttpContent content = new StringContent(product.ToString(), Encoding.UTF8, "application/json");
            string content = JsonConvert.SerializeObject(product);

            HttpResponseMessage response = client.PutAsJsonAsync("https://localhost:44324/api/Products/" + product.ProductId, content).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error while trying to update an product " + response.Content);
            }
        }

        //public static DataTable AllProducts()
        //{

        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        conn.Open();

        //        // 1.  create a command object identifying the stored procedure
        //        SqlCommand cmd = new SqlCommand("AllProducts", conn);

        //        // 2. set the command object so it knows to execute a stored procedure
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //        // execute the command
        //        using (SqlDataReader rdr = cmd.ExecuteReader())
        //        {
        //            DataTable dt = new DataTable();
        //            dt.Load(rdr);
        //            return dt;
        //        }
        //    }
        //}


        //static async Task RunAsync()
        //{
        //    // Update port # in the following line.
        //    client.BaseAddress = new Uri("https://localhost:44324");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //    try
        //    {
        //        //var url = await CreateProductAsync(product);
        //        //Console.WriteLine($"Created at {url}");

        //        // Get the product
        //        DataTable dt = await GetProductAsync("https://localhost:44324/api/Products");

        //        // Update the product
        //        //Console.WriteLine("Updating price...");
        //        //product.Price = 80;
        //        //await UpdateProductAsync(product);

        //        //// Get the updated product
        //        //product = await GetProductAsync(url.PathAndQuery);
        //        //ShowProduct(product);

        //        //// Delete the product
        //        //var statusCode = await DeleteProductAsync(product.Id);
        //        //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //    Console.ReadLine();
        //}


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
