using Autofac;
using ShoppingCart.Common.Configuration;
using ShoppingCart.Common.Logging;

namespace ShoppingCart.Common.Dependency
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();
            builder.RegisterType<WebApiAppSettings>().As<IWebApiAppSettings>().SingleInstance();

            base.Load(builder);
        }
    }
}
