using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mah9.Startup))]
namespace mah9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
