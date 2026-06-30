using Crud.Domain;
using Crud.Infrastructure.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository (IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public long CreateProduct(Product product)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("CreateProduct", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CategoryName", product.CategoryName);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
            cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
            cmd.Parameters.AddWithValue("@CreatedBy", 1);
            cmd.Parameters.AddWithValue("@UpdatedBy", 1);

            long result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public long DeleteProduct(long id)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("DeleteProduct", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProductId", id);
            cmd.Parameters.AddWithValue("@UpdatedBy", 1);

            long result = cmd.ExecuteNonQuery();

            con.Close();
            return result;
        }

        public List<Product> GetAllProducts()
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("GetProducts", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            List<Product> result = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product
                {
                    ProductId = Convert.ToInt64(reader["ProductId"]),
                    CategoryName = reader["CategoryName"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"])
                };
                result.Add(product);
            }
            reader.Close();
            con.Close();
            return result;

        }

        public Product GetProductById(long id)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("GetProductById", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProductId", id);

            SqlDataReader reader = cmd.ExecuteReader();
            Product product = null;
            while (reader.Read())
            {
                product = new Product
                {
                    ProductId = Convert.ToInt64(reader["ProductId"]),
                    CategoryName = reader["CategoryName"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"])
                };
            }
            reader.Close();
            con.Close();
            return product;

        }

        public long UpdateProduct(Product product)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("UpdateProduct", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
            cmd.Parameters.AddWithValue("@CategoryName", product.CategoryName);
            cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
            cmd.Parameters.AddWithValue("@ProductPrice", product.ProductPrice);
            cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
            cmd.Parameters.AddWithValue("@UpdatedBy", 1);

            long result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public List<Product> SearchProducts(string categoryName, string productName)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("SearchProducts", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CategoryName", string.IsNullOrEmpty(categoryName) ? (object)DBNull.Value : categoryName);
            cmd.Parameters.AddWithValue("@ProductName", string.IsNullOrEmpty(productName) ? (object)DBNull.Value : productName);

            SqlDataReader reader = cmd.ExecuteReader();
            List<Product> result = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product
                {
                    ProductId = Convert.ToInt64(reader["ProductId"]),
                    CategoryName = reader["CategoryName"].ToString(),
                    ProductName = reader["ProductName"].ToString(),
                    ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"])
                };
                result.Add(product);
            }
            reader.Close();
            con.Close();
            return result;
        }

        public List<string> GetAllCategoryNames()
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("GetCategoryNames", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            List<string> result = new List<string>();
            while (reader.Read())
            {
                result.Add(reader["CategoryName"].ToString());
            }
            reader.Close();
            con.Close();
            return result;
        }

        public List<string> GetAllProductNames()
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("GetProductNames", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            List<string> result = new List<string>();
            while (reader.Read())
            {
                result.Add(reader["ProductName"].ToString());
            }
            reader.Close();
            con.Close();
            return result;
        }
    }
}
