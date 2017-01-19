using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace ShoppingCart.Common.Configuration
{
    [ExcludeFromCodeCoverage]
    public class WebApiAppSettings : IWebApiAppSettings
    {
        public string WebApiUsername
        {
            get { return ConfigurationManager.AppSettings.Get("WebApiUsername"); }
        }

        public string WebApiPassword
        {
            get { return ConfigurationManager.AppSettings.Get("WebApiPassword"); }
        }

        public string ClientWebApiPassword
        {
            get { return ConfigurationManager.AppSettings.Get("ClientWebApiPassword"); }
        }

        public int MaximumPageSize => Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaximumPageSize"));

        public int DefaultPageSize => Convert.ToInt32(ConfigurationManager.AppSettings.Get("DefaultPageSize"));
    }
}