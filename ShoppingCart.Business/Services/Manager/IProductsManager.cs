using System.Collections;
using System.Collections.Generic;
using ShoppingCart.Models;

namespace ShoppingCart.Business.Services.Manager
{
    public interface IProductsManager
    {
        void AddProducts(Product product);

       IEnumerable<Product> GetProducts();

       Product GetProducts(int quantity, string name);

        bool UpdatedProduct(Product product);

        void RemoveProduct(Product product);

        IEnumerable<Product> GetProductsByPaging(int skip, int pageSize);
    }
}
