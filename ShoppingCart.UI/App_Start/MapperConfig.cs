using ExpressMapper;
using ShoppingCart.UI.Models;

namespace ShoppingCart.UI
{
    public static class MapperConfig
    {
        public static void  RegisterMapping()
        {
            Mapper.Register<Product, ShoppingCart.Models.Product>();               
            Mapper.Compile();
        }
    }
}