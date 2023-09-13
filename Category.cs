using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermarket
{
    internal class Category
    {
        public int Id  { get; set; }
        public string CategoryName { get; set; }
        public int NUmberOfProduct { get; set; }
        public Category(int id,string categoryname,int number) 
        {
           Id = id;
           CategoryName = categoryname;
            NUmberOfProduct = number;
        }

    }
}
