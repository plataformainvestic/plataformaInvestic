using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(INI.Startup))]
namespace INI
{    
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
