using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BITCollegeSite.Startup))]
namespace BITCollegeSite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
