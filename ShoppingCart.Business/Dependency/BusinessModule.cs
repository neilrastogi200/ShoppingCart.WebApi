using Autofac;
using ShoppingCart.Business.Services.Manager;
using ShoppingCart.Repository.Dependency;

namespace ShoppingCart.Business.Dependency
{
    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductsManager>()
                .As<IProductsManager>().InstancePerDependency();

            builder.RegisterModule(new RepositoryModule());

            base.Load(builder);
        }
    }
}
