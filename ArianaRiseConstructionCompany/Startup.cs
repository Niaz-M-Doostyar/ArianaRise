using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArianaRiseConstructionCompany.Startup))]
namespace ArianaRiseConstructionCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
