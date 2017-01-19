using System.Collections.Generic;
using ShoppingCart.Models;

namespace ShoppingCart.Repository.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product Get(string name, int quantity);
        void Add(Product item);
        void Remove(int id);
        bool Update(Product item);
        IEnumerable<Product> GetAllByPaging(int skip, int pageSize);

    }
}
