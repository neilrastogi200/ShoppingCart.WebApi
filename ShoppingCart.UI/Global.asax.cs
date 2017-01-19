using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using ShoppingCart.Common.Logging;
using ShoppingCart.UI.Handlers;

namespace ShoppingCart.UI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            MapperConfig.RegisterMapping();
            DependencyConfig.RegisterDependencyResolvers();
            ILogger logger = new Logger();
            GlobalConfiguration.Configuration.MessageHandlers.Add(new BasicAuthenticationDelegatingHandler(GlobalConfiguration.DefaultServer,logger));
        }
    }
}
