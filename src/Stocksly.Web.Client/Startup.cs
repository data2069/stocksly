using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stocksly.Web.Client.Startup))]
namespace Stocksly.Web.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
