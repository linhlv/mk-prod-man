using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kenrapid.CRM.Web.Startup))]
namespace Kenrapid.CRM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
