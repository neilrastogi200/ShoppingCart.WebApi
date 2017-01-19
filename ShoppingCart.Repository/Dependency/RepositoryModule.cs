using Autofac;
using ShoppingCart.Repository.Repository;

namespace ShoppingCart.Repository.Dependency
{
    public sealed class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
