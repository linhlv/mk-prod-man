using System.Security.Principal;
using Microsoft.AspNet.Identity;
using Kenrapid.CRM.Web.Data;
using Kenrapid.CRM.Web.Domain;

namespace Kenrapid.CRM.Web.Infrastructure
{
	public class CurrentUser : ICurrentUser
	{
		private readonly IIdentity _identity;
		private readonly KenrapidDbContext _context;

		private ApplicationUser _user;

        public CurrentUser(IIdentity identity, KenrapidDbContext context)
		{
			_identity = identity;
			_context = context;
		}

		public ApplicationUser User
		{
			get { return _user ?? (_user = _context.Users.Find(_identity.GetUserId())); }
		}
	}
}