using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Models;

namespace ShoppingCart.Repository.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();
        private int _nextId = 1;

        public ProductRepository()
        {
            Add(new Product { Name = "milk", Price = 1.39M, ProductId = 1, Quantity = 1});
            Add(new Product { Name = "Pepsi", Price = 3.75M,ProductId = 2, Quantity = 2});
            Add(new Product { Name = "Coke", Price = 0.89M, ProductId = 3, Quantity = 1});
            Add(new Product { Name = "Lemonade", Price = 0.88M, ProductId = 4, Quantity = 1 });
            Add(new Product { Name = "Orange Juice", Price = 0.89M, ProductId = 5, Quantity = 1 });
            Add(new Product { Name = "Apple Juice", Price = 0.78M, ProductId = 6, Quantity = 1 });
            Add(new Product { Name = "Mango Juice", Price = 0.69M, ProductId = 7, Quantity = 1 });
            Add(new Product { Name = "Diet Coke", Price = 0.78M, ProductId = 9, Quantity = 1 });
            Add(new Product { Name = "Lassi", Price = 0.78M, ProductId = 10, Quantity = 1 });
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public IEnumerable<Product> GetAllByPaging(int skip, int pageSize)
        {
            return _products.Skip(skip).Take(pageSize);
        }

        public Product Get(string name, int quantity)
        {
            return _products.Find(p => p.Name == name && p.Quantity == quantity);
        }

        public void Add(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("This product does not exist");
            }

            item.ProductId = _nextId++;
            _products.Add(item);
        }

        public void Remove(int id)
        {
            _products.RemoveAll(p => p.ProductId == id);
        }

        public bool Update(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("This product does not exist");
            }
            int index = _products.FindIndex(p => p.ProductId == item.ProductId);

            if (index == -1)
            {
                return false;
            }

            foreach (var updatedItem in _products)
            {
                if (updatedItem.ProductId == item.ProductId)
                {
                    updatedItem.Quantity = item.Quantity;
                }
            }

            return true;
        }
    }
}