using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OrderService.Startup))]
namespace OrderService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
