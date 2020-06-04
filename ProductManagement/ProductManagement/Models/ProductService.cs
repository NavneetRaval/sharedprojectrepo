using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class ProductService
    {
        public SqlConnection con;

        private void connection()
        {
            string constr = @"Data Source=DESKTOP-3SORUJN;Initial Catalog=Product;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con = new SqlConnection(constr);
        }
        public bool DeleteProductInfoById(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ProductId", id);
                connection();
                con.Open();
                con.Execute("DeleteProductById", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public bool SaveProductInformation(Product productModel)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ProductId", productModel.ProductId);
                param.Add("@ProductName", productModel.ProductName);
                param.Add("@Price", productModel.Price);
                param.Add("@Quantity", productModel.Quantity);
                connection();
                con.Open();
                con.Execute("AddUpdateProductDetails", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Product> GetProductInfo(string FilterText)
        {
            connection();
            con.Open();
            DynamicParameters param = new DynamicParameters();
            param.Add("@FilterText", FilterText);
            IList<Product> EmpList = SqlMapper.Query<Product>(con, "GetProductData", param, commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return EmpList.ToList();
        }
    }
}