using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IEP.Startup))]
namespace IEP
{    
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
