using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace HeroicCRM.Web.Models
{
	public class CustomerRiskViewModel :IMapFrom<Kenrapid.CRM.Web.Domain.Risk>
	{
		public string Title { get; set; }

		public string Description { get; set; }
	}
}