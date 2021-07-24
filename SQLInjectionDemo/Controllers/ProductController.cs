using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLInjectionDemo.ViewModels;

namespace SQLInjectionDemo
{
    public class ProductController : Controller
    {
        public ActionResult Index(string SearchString)
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                string connString = @"Server=YOUR_SERVER;Database=BookShopDb;Trusted_Connection=True;";
                string query = "select * from Products WHERE Name= '" + SearchString + "'";

                DataTable dataTable = new DataTable();

                SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);

                conn.Close();
                da.Dispose();

                var productsList = GetProductsList(dataTable);

                var vm = new ProductViewModel()
                {
                    Products = productsList
                };

                return View(vm);
            }
            return View();
        }

        private List<Product> GetProductsList(DataTable table)
        {
            var productList = new List<Product>(table.Rows.Count);
            foreach (DataRow row in table.Rows)
            {
                var values = row.ItemArray;
                var product = new Product()
                {
                    Id = (Guid)values[0],
                    Name = (string)values[1],
                    Price = (decimal)values[2]
                };
                productList.Add(product);
            }

            return productList;
        }
    }
}
