using Kenrapid.CRM.Web.Domain;

namespace Kenrapid.CRM.Web.Infrastructure
{
	public interface ICurrentUser
	{
		ApplicationUser User { get; } 
	}
}