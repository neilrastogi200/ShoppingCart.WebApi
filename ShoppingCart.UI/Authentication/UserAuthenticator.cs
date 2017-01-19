using ShoppingCart.Common.Configuration;

namespace ShoppingCart.UI.Authentication
{
    public class UserAuthenticator : IUserAuthenticator
    {
        private readonly IWebApiAppSettings _appSettings;

        public UserAuthenticator(IWebApiAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        // Validate a Web API Username and password
        public bool IsValid(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return false;
            return username == _appSettings.WebApiUsername && password == _appSettings.WebApiPassword;
        }
    }
}