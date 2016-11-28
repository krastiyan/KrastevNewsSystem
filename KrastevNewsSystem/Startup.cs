using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KrastevNewsSystem.Startup))]
namespace KrastevNewsSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
