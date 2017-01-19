using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using ShoppingCart.Business.Dependency;
using ShoppingCart.Common.Dependency;
using ShoppingCart.UI.Authentication;
using ShoppingCart.UI.Validation;
using ShoppingCart.UI.Validation.Interfaces;

namespace ShoppingCart.UI
{
    public sealed class DependencyConfig
    {
        private static IContainer _container; 
        public static IContainer Container => _container ?? RegisterDependencyResolvers();

        public static IContainer RegisterDependencyResolvers()
        {
            var builder = new ContainerBuilder();
            RegisterAutofacFramework(builder);
            RegisterApplicationDependencies(builder);

            _container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(_container);

            return _container;
        }

        private static void RegisterAutofacFramework(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterModule(new AutofacWebTypesModule());
        }

        private static void RegisterApplicationDependencies(ContainerBuilder builder)
        {
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new BusinessModule());
            builder.RegisterType<UserAuthenticator>().As<IUserAuthenticator>().InstancePerLifetimeScope();
            builder.RegisterType<DataAnnotation>().As<IDataAnnotation>().InstancePerRequest();
        }
    }
}