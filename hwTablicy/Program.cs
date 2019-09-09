using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace hwTablicy
{
    class Program
    {
        public static string connectionString = @"Data Source=L206-3\SQLEXPRESS;Initial Catalog=ShopDB;Integrated Security=True";
        public static string commandString = "SELECT * FROM Customers; SELECT * FROM Employees; SELECT * FROM OrderDetails; SELECT * FROM Orders; SELECT * FROM Products;";
        static void Main(string[] args)
        {
            CreateDataSet();
        }
        static void CreateDataSet()
        {
            DataSet shopDB = new DataSet("ShopDB");
            SqlDataAdapter adapter = new SqlDataAdapter(commandString, connectionString);
            adapter.Fill(shopDB);

            DataTable customers = shopDB.Tables[0];
            DataTable employees = shopDB.Tables[1];
            DataTable orderDetails = shopDB.Tables[2];
            DataTable orders = shopDB.Tables[3];
            DataTable products = shopDB.Tables[4];

            var customerOrderRel = new DataRelation("Customers_Order", customers.Columns["CustomerNo"], orders.Columns["CustomerNo"], true);
            var employeeOrderRel = new DataRelation("Employee_Order", employees.Columns["EmployeeID"], orders.Columns["EmployeeID"], true);
            var productsOrderDetailsRel = new DataRelation("Products_OrderDetails", products.Columns["ProdID"], orderDetails.Columns["ProdID"], true);

            shopDB.Relations.Add(productsOrderDetailsRel);
            shopDB.Relations.Add(employeeOrderRel);
            shopDB.Relations.Add(customerOrderRel);
        }
    }
    
}
