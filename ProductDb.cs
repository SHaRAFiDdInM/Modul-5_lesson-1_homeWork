using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    internal class ProductDb
    {
        public void CreateProduct(string name, decimal price, int categoryId )
        {

            string command = $"INSERT INTO dbo.Product (Product_Name,Price,Category_id) VALUES ('{name}','{price}','{categoryId}')";
            DataAccessLayer.ExecuteNonQuery(command);
        }
        public void DeleteProduct(int id)
        {
            string command = $"DELETE dbo.Product WHERE ID = {id}";
            DataAccessLayer.ExecuteNonQuery(command);
        }
        public void UpdateProduct(int id, string newname, decimal Newprice,int categoryId)
        {
            string command = $"UPDATE dbo.Product SET Product_Name = '{newname}',Price = '{Newprice}' WHERE Id = {id}"+
                $"Update dbo.category set Number_Of_Products +=1 where id = {categoryId}";
            DataAccessLayer.ExecuteNonQuery(command);
        }
        public void ReadAllProduct()
        {
            string command = $"Select * from dbo.Product;";
            ExecuteQuery(command);

        }

        public void ReadBYIdProduct(int id)
        {
            string command = $"Select * From dbo.Product Where Id = {id}";
            ExecuteQuery(command);

        }
        public void ReadProductByName(string name)
        {
            string command = $"Select * From Product Where Product_Name = '{name}'";
            ExecuteQuery(command);

        }
        public void ReadByPrice(decimal price)
        {
            string command = $"Select * from Product Where Price > {price}";
            ExecuteQuery(command);
        }
        private static List<Product> ExecuteQuery(string query)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessLayer.Connection_String))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    products = ReadProductFromDataReader(command.ExecuteReader());
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

            return products;
        }

        private static List<Product> ReadProductFromDataReader(SqlDataReader reader)
        {
            List<Product> products = new List<Product>();
            if (reader == null)
            {
                return products;
            }

            if (!reader.HasRows)
            {
                Console.WriteLine("No data.");
                return products;
            }

            Console.WriteLine("{0}\t{1}\t{2}    \t{3}",
                    reader.GetName(0),
                    reader.GetName(1),
                    reader.GetName(2),
                    reader.GetName(3));


            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string productName = reader.GetString(1);
                decimal price = reader.GetDecimal(2);
                int categoryId = reader.GetInt32(3);

                products.Add(new Product(id, productName, price, categoryId));

                Console.WriteLine("{0}\t{1}      \t{2}     \t{3}", id, productName, price, categoryId);
            }
            reader.Close();

            return products;
        }

    }
}
