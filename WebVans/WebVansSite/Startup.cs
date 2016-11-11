using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebVansSite.Startup))]
namespace WebVansSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
