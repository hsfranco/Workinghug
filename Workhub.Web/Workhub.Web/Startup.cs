using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Workhub.Web.Startup))]
namespace Workhub.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
