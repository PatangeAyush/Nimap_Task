using CRUD_Operation_Task.Models;
using System.Data;
using System.Data.SqlClient;

namespace CRUD_Operation_Task
{
    public class Logic
    {
        private readonly IConfiguration configuration;

        public Logic(IConfiguration configuration)
        {
            this.configuration=configuration;
        }

        public string GetConnectionString()
        {
           return configuration.GetConnectionString("MySqlCS").ToString();
        }

        #region -- Index Page Logic --
        public List<ProductCombinedViewModel> GetCombinedProductCategoryData()
        {
            List<ProductCombinedViewModel> combinedData = new List<ProductCombinedViewModel>();

            using (SqlConnection con = new SqlConnection(this.GetConnectionString()))
            {
                string query = @"SELECT p.ProductID, p.ProductName, c.CategoryID, c.CategoryName 
                         FROM Products p 
                         LEFT JOIN Categories c ON p.CategoryID = c.CategoryID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        combinedData.Add(new ProductCombinedViewModel
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            ProductName = reader["ProductName"].ToString(),
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            CategoryName = reader["CategoryName"].ToString()
                        });
                    }
                    con.Close();
                }
            }

            return combinedData;
        }
#endregion

        #region -- Product CRUD --

        public List<ProductDTO> GetProducts()
        {
            List<ProductDTO> GetList = new List<ProductDTO>();

            using (SqlConnection con = new SqlConnection(this.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("GetProductSP", con))
                {
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        GetList.Add(new ProductDTO
                        {
                            ProductID = Convert.ToInt32(dr["ProductID"]),
                            ProductName = dr["ProductName"].ToString(),
                            CategoryID = Convert.ToInt32(dr["CategoryID"])
                        });
                    }

                }
            }
            return GetList;
        }

        public void AddProduct(ProductDTO add)
        {
            using (SqlConnection con = new SqlConnection(this.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("AddProductSP", con))
                {
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductName", add.ProductName);
                    cmd.Parameters.AddWithValue("@CategoryID", add.CategoryID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(ProductDTO update)
        {
            using (SqlConnection con = new SqlConnection(this.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateProductSP", con))
                {
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductID", update.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", update.ProductName);
                    cmd.Parameters.AddWithValue("@CategoryID", update.CategoryID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(this.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteProductSP", con))
                {
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductID",id);          

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region -- Categories CRUD --

        public List<CategoryDTO> GetCategories()
        {
            List<CategoryDTO> GetList = new List<CategoryDTO>();

            using (SqlConnection con = new SqlConnection(this.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("GetCategoriesSP", con))
                {
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        GetList.Add(new CategoryDTO
                        {
                            CategoryName = dr["CategoryName"].ToString(),
                            CategoryID = Convert.ToInt32(dr["CategoryID"])
                        });
                    }

                }
            }
            return GetList;
        }

        public void AddCategory(string CategoryName)
        {
            using (SqlConnection con = new SqlConnection(this.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("AddCategorySP", con))
                {
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCategory(CategoryDTO update)
        {
            using (SqlConnection con = new SqlConnection(this.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateCategorySP", con))
                {
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CategoryID", update.CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", update.CategoryName);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(this.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteCategorySP", con))
                {
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CategoryID", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region -- DropDown List Logic --

        public List<CategoryDTO> GetProductCategories()
        {
            List<CategoryDTO> CategoriesList = new List<CategoryDTO>();

            using (SqlConnection con = new SqlConnection(this.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Categories", con))
                {
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        CategoriesList.Add(new CategoryDTO
                        {
                            CategoryID = Convert.ToInt32(dr["CategoryID"]),
                            CategoryName = dr["CategoryName"].ToString()
                        });
                    }
                }
            }
            return CategoriesList;
        }
        #endregion  
    }
}
