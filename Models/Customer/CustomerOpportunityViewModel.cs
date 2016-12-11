using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure.Mapping;
namespace Kenrapid.CRM.Web.Models
{
    public class CustomerOpportunityViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.Opportunity>
	{
		public string Title { get; set; }

		public string Description { get; set; }
	}
}