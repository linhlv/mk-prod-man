using System.Security.Principal;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using StructureMap.Configuration.DSL;

namespace Kenrapid.CRM.Web.Infrastructure.DependencyRegistry
{
	public class MvcRegistry : Registry
	{
		public MvcRegistry()
		{
            For<IIdentity>().Use(() => HttpContext.Current.User.Identity);
			For<BundleCollection>().Use(BundleTable.Bundles);
			For<RouteCollection>().Use(RouteTable.Routes);			
			For<HttpSessionStateBase>()
				.Use(() => new HttpSessionStateWrapper(HttpContext.Current.Session));
			For<HttpContextBase>()
				.Use(() => new HttpContextWrapper(HttpContext.Current));
			For<HttpServerUtilityBase>()
				.Use(() => new HttpServerUtilityWrapper(HttpContext.Current.Server));
		}
	}
}