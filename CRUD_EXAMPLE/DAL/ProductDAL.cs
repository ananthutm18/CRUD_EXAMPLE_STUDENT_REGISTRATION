using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CRUD_EXAMPLE.Models;
namespace CRUD_EXAMPLE.DAL
{
    public class ProductDAL
    {

        string conString = ConfigurationManager.ConnectionStrings["mycrudconnectionstring"].ToString();


        //Get all products

        public List<Product> GetAllProduct()
        {
            List<Product> productList = new List<Product>();
            
            using(SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllProducts";

                SqlDataAdapter sqlDA= new SqlDataAdapter(command);  
                DataTable dtProducts = new DataTable(); 
                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close(); 
                foreach(DataRow dr in dtProducts.Rows)
                {

                    productList.Add(new Product { 
                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remarks = dr["Remarks"].ToString()

                    });

                }
            }


            return productList;
        }
    }
}