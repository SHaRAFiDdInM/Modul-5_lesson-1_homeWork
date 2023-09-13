using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategpryID { get; set; }
        public Product(int id, string name, decimal price, int categpryID)
        {
            Id = id;
            Name = name;
            Price = price;
            CategpryID = categpryID;
        }
    }
}
