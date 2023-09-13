using System;

namespace Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductDb product = new ProductDb();

            //product.CreateProduct("Coca-Cola",10000,1);
            //product.ReadBYIdProduct(1);
            CategoryDbService category = new CategoryDbService();
            category.GetAllCategories();

            
            
            Console.ReadKey();
        }
    }
}
