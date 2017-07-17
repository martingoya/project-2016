using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OhlalaWebWithAuth.Startup))]
namespace OhlalaWebWithAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
