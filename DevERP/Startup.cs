using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevERP.Startup))]
namespace DevERP
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
