using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System;
namespace Kenrapid.CRM.Web.Models.Opportunity
{
    public class OpportunityViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.Opportunity>
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime CreateDate { get; set; }

		public int CustomerId { get; set; }

		public string CustomerName { get; set; }
	}
}