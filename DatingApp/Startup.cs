using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DatingApp.Startup))]
namespace DatingApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
