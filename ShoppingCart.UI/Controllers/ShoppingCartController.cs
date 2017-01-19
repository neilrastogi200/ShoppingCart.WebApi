using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ExpressMapper;
using ShoppingCart.Business.Services.Manager;
using ShoppingCart.Common.Configuration;
using ShoppingCart.Common.Logging;
using ShoppingCart.UI.Models;
using ShoppingCart.UI.Validation.Interfaces;

namespace ShoppingCart.UI.Controllers
{
    public class ShoppingCartController : ApiController
    {
        private readonly IWebApiAppSettings _apiAppSettings;
        private readonly IDataAnnotation _dataAnnotation;
        private readonly ILogger _logger;
        private readonly IProductsManager _productsManager;

        public ShoppingCartController(IProductsManager productsManager, IDataAnnotation dataAnnotation, ILogger logger,
            IWebApiAppSettings apiAppSettings)
        {
            _productsManager = productsManager;
            _dataAnnotation = dataAnnotation;
            _logger = logger;
            _apiAppSettings = apiAppSettings;
        }


        [HttpPost]
        [Route("products")]
        public IHttpActionResult Post(Product product)
        {
            if (product != null)
            {
                var success = ValidateProduct(product);

                if (success)
                {
                    var mapped = Mapper.Map<Product, ShoppingCart.Models.Product>(product);
                    _productsManager.AddProducts(mapped);
                    return Ok("Successfully Addedd");
                }
            }

            return BadRequest("A problem occurred whilst adding your product");
        }

        [HttpGet]
        [Route("products")]
        public IEnumerable<ShoppingCart.Models.Product> Get()
        {
            _logger.Log("[ShoppingCartController::Get] Entering method body",
                LogLevel.Info);

            var products = _productsManager.GetProducts();

            return products;
        }


        [HttpGet]
        [Route("ShoppingCart/products/{pageNo}/{pageSize}")]
        public IHttpActionResult Get(int pageNo, int pageSize)
        {
            var pageSizeCheck = GetPageSize(pageSize);

            var skip = Skip(pageNo, pageSizeCheck);

            var products = _productsManager.GetProductsByPaging(skip, pageSizeCheck);

            var mapped = Mapper.Map<IEnumerable<ShoppingCart.Models.Product>, IEnumerable<Product>>(products);

            return Ok(new PagedDataResultContract<Product>(mapped, pageNo, pageSize, mapped.Count()));
        }

        [HttpGet]
        [Route("products/{quantity}/{name}")]
        public ShoppingCart.Models.Product Get(int quantity, string name)
        {
            var products = _productsManager.GetProducts(quantity, name);

            return products;
        }

        [HttpPut]
        [Route("products")]
        public IHttpActionResult Put(Product product)
        {
            var mapped = Mapper.Map<Product, ShoppingCart.Models.Product>(product);
            var success = _productsManager.UpdatedProduct(mapped);

            if (!success)
            {
                return BadRequest("It has failed to update the product with id" + product.ProductId);
            }

            return Ok("The product has been successfully updated");
        }

        [HttpDelete]
        [Route("products")]
        public IHttpActionResult Delete(Product product)
        {
            var mapped = Mapper.Map<Product, ShoppingCart.Models.Product>(product);
            _productsManager.RemoveProduct(mapped);

            return Ok("Successfully Deleted");
        }


        private bool ValidateProduct(Product product)
        {
            var validationResult = _dataAnnotation.ValidateEntity(product);

            if (validationResult.HasError)
            {
                _logger.Log("This product is invalid" + product.ProductId, LogLevel.Warn);
                return false;
            }

            return true;
        }

        private static int Skip(int pageNo, int pageSize)
        {
            var skip = (pageNo - 1)*pageSize;

            if (skip < 0)
            {
                skip = 0;
            }
            return skip;
        }


        private int GetPageSize(int pageSize)
        {
            if (pageSize == 0) return _apiAppSettings.DefaultPageSize;

            return pageSize > _apiAppSettings.MaximumPageSize ? _apiAppSettings.MaximumPageSize : pageSize;
        }
    }
}