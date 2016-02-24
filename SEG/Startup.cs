using Microsoft.Owin;
using Owin;
using SEG;

[assembly: OwinStartup(typeof (Startup))]

namespace SEG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}