using System;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.Risk
{
    public class RiskViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.Risk>
	{
		public int CustomerId { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime CreateDate { get; set; }

		public string CustomerName { get; set; }
	}
}