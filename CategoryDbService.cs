using System.Data.SqlClient;
using System;
using System.Security.Permissions;
using System.Collections.Generic;

namespace Supermarket
{
    internal class CategoryDbService
    {
        public void CreateCategory(string categoryName)
        {
            string command = $"Insert Into dbo.Category (CategoryName) values ('{categoryName}')";
            DataAccessLayer.ExecuteNonQuery(command);
        }
        public void DeleteCategory(int id)
        {       
             string command = $"DELETE dbo.Category WHERE ID = {id}";
             DataAccessLayer.ExecuteNonQuery(command);
        }

        public void UpdateCategory(int id, string NewcategoryName)
        {
            string command = $"Update dbo.category set CategoryName = '{NewcategoryName}' where Id ={id}";
        }
        public void GetAllCategories()
        {
            string query = "SELECT * FROM dbo.Category;";

            ExecuteQuery(query);
        }

        public void GetCategoryById(int id)
        {
            string query = "SELECT * FROM dbo.Category" +
                $" WHERE Id = {id};";

            ExecuteQuery(query);
        }

        public void GetCategoryByName(string categoryName)
        {
            string query = "SELECT * FROM dbo.Category" +
                $" WHERE Category_Name LIKE '%{categoryName}%';";

            ExecuteQuery(query);
        }

        private static List<Category> ExecuteQuery(string query)
        {
            List<Category> categories = new List<Category>();
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessLayer.Connection_String))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    categories = ReadCategoryFromDataReader(command.ExecuteReader());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong while reading products. {ex.Message}.");
            }

            return categories;
        }

        private static List<Category> ReadCategoryFromDataReader(SqlDataReader reader)
        {
            List<Category> categories = new List<Category>();
            if (reader == null)
            {
                return categories;
            }

            if (!reader.HasRows)
            {
                Console.WriteLine("No data.");
                return categories;
            }

            Console.WriteLine("{0}\t{1}\t{2}",
                    reader.GetName(0),
                    reader.GetName(1),
                    reader.GetName(2));

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int numberOfProducts = reader.GetInt32(2);

                categories.Add(new Category(id, name, numberOfProducts));

                Console.WriteLine("{0} \t{1}   \t{2}", id, name, numberOfProducts);
            }
            reader.Close();

            return categories;
        }

    }
}