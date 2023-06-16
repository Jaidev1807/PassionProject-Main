using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassionProject_Main.Startup))]
namespace PassionProject_Main
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
