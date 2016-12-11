using System.Data.Entity;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using StructureMap.Configuration.DSL;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Data;

namespace Kenrapid.CRM.Web.Infrastructure.DependencyRegistry
{
	public class AspNetIdentityRegistry : Registry
	{
		public AspNetIdentityRegistry()
		{
            For<IUserStore<ApplicationUser>>().Use<UserStore<ApplicationUser>>();
            For<DbContext>().Use<KenrapidDbContext>();
			For<IAuthenticationManager>().Use(ctx => ctx.GetInstance<HttpRequestBase>().GetOwinContext().Authentication);
		}
	}
}