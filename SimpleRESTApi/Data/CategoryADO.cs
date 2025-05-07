using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CategoryADO : ICategory
    {
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;
        public CategoryADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }
        public Category AddCategory(Category category)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"INSERT INTO Categories (CategoryName)
                                VALUES (@CategoryName);select SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                try
                {
                  
                    cmd.Parameters.AddWithValue("@CategoryName",category.CategoryName);
                    conn.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    int categoryId = Convert.ToInt32(cmd.ExecuteScalar());
                    category.CategoryId = categoryId;
                    return category;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                       cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void DeleteCategory(int categoryId) 
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"DELETE FROM Categories WHERE CategoryId = @CategoryId";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                try{
                    cmd.Parameters.AddWithValue("@CategoryId",categoryId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if(result==0){
                        throw new Exception("Category not found");
                    }
                }
                catch(Exception ex){
                    throw new Exception(ex.Message);
                }
                finally{
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public IEnumerable<Category> GetCategories()
        {
             List<Category> categories = new List<Category>();
           using (SqlConnection conn = new SqlConnection(connStr))
           {
            string strSql = @"SELECT * FROM Categories ORDER BY CategoryName";
            SqlCommand cmd = new SqlCommand(strSql,conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    Category category = new();
                    category.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    category.CategoryName = dr["CategoryName"].ToString();
                    categories.Add(category);
                }
            }
            dr.Close();
            cmd.Dispose();
            conn.Close();
           }
           return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            Category category = new Category();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"SELECT * FROM Categories WHERE CategoryId = @CategoryId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    dr.Read();
                    category.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    category.CategoryName = dr["CategoryName"].ToString();
                }
                else
                {
                    throw new Exception("Category not found");
                }
                 
            cmd.Dispose();
            conn.Close();

            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"UPDATE Categories SET CategoryName = @CategoryName 
                WHERE CategoryId = @CategoryId";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CategoryName",category.CategoryName);
                    cmd.Parameters.AddWithValue("@CategoryId",category.CategoryId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if(result == 0)
                    {
                        throw new Exception("Category not found");
                    }
                    return category;
                }
                catch (Exception ex){
                    throw new Exception(ex.Message);
                }
                finally{
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }
}