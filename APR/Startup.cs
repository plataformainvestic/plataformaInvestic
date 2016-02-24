using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(APR.Startup))]
namespace APR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
