using Autofac;
using Autofac.Integration.Mvc;
using OrderService.DataAccess.Handlers;
using OrderService.DataAccess.Providers;
using OrderService.DataAccess.Validators;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OrderService
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
            RouteConfig.RegisterRoutes( RouteTable.Routes );
            BundleConfig.RegisterBundles( BundleTable.Bundles );

            var builder = new ContainerBuilder();

            builder.RegisterControllers( Assembly.GetExecutingAssembly() );

            builder.RegisterType<ProductProvider>().As<IProductProvider>();
            builder.RegisterType<ProductHandler>().As<IProductHandler>();
            builder.RegisterType<ProductValidator>().As<IProductValidator>();

            builder.RegisterType<DeliveryProvider>().As<IDeliveryProvider>();
            builder.RegisterType<DeliveryValidator>().As<IDeliveryValidator>();
            builder.RegisterType<DeliveryHandler>().As<IDeliveryHandler>();

            builder.RegisterType<ClientOrderProvider>().As<IClientOrderProvider>();
            builder.RegisterType<ClientOrderValidator>().As<IClientOrderValidator>();
            builder.RegisterType<ClientOrderHandler>().As<IClientOrderHandler>();

            builder.RegisterType<UserProvider>().As<IUserProvider>();

            DependencyResolver.SetResolver( new AutofacDependencyResolver( builder.Build() ) );
        }

        // https://stackoverflow.com/questions/32236013/asp-net-mvc-binding-decimal-value/32236920
        // ASP.NET MVC binding decimal value
        public void Application_AcquireRequestState( object sender, EventArgs e ) {
            var culture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = culture.NumberFormat.CurrencyDecimalSeparator = culture.NumberFormat.PercentDecimalSeparator = ".";
            culture.NumberFormat.NumberGroupSeparator = culture.NumberFormat.CurrencyGroupSeparator = culture.NumberFormat.PercentGroupSeparator = ",";
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }
}
