using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Product
    {
        public int ProductId { set; get; }

        public string Name { set; get; }

        public int Quantity { set; get; }

        public decimal Price { set; get; }

        public Product()
        {
            
        }

        public Product(int quantity, int productId)
        {
            Quantity = quantity;
            ProductId = productId;
        }

        public decimal TotalPrice => Quantity * Price;

        public bool IsValid()
        {
            if (ProductId < 0)
            {
                return false;
            }

            if (Price < 0)
            {
                return false;
            }

            return true;
        }
    }
}
