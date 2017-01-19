using System.Collections.Generic;
using ShoppingCart.Common.Logging;
using ShoppingCart.Models;
using ShoppingCart.Repository.Repository;

namespace ShoppingCart.Business.Services.Manager
{
    public class ProductsManager : IProductsManager
    {
        private readonly ILogger _logger;
        private readonly IProductRepository _productRepository;

        public ProductsManager(IProductRepository productRepository, ILogger logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public void AddProducts(Product product)
        {
            _logger.Log("[ProductsManager::AddProducts] Entering method body with " + product.ProductId + " messages.",
                LogLevel.Info);

            IList<Product> invalidProducts = new List<Product>();

            if (!product.IsValid())
            {
                invalidProducts.Add(product);
                _logger.Log("The following Product is invalid " + product.ProductId, LogLevel.Warn);
            }
            else
            {
                _productRepository.Add(product);
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetAll();
        }

        public Product GetProducts(int quantity, string name)
        {
            return _productRepository.Get(name, quantity);
        }

        public bool UpdatedProduct(Product product)
        {
            return _productRepository.Update(product);
        }

        public void RemoveProduct(Product product)
        {
            _productRepository.Remove(product.ProductId);
        }

        public IEnumerable<Product> GetProductsByPaging(int skip, int pageSize)
        {
            return _productRepository.GetAllByPaging(skip,pageSize);
        }
    }
}