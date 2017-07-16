using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Oh_lala_Web.Startup))]
namespace Oh_lala_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
