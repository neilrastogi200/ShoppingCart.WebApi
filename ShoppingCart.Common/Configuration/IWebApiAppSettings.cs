
namespace ShoppingCart.Common.Configuration
{
    public interface IWebApiAppSettings
    {
        string WebApiUsername { get; }
        string WebApiPassword { get; }
        string ClientWebApiPassword { get; }
        int MaximumPageSize { get; }
        int DefaultPageSize { get; }
       
    }
}