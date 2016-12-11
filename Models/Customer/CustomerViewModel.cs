using HeroicCRM.Web.Models;
using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;

namespace Kenrapid.CRM.Web.Models
{
    public class CustomerViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.Customer>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string WorkEmail { get; set; }

		public string HomeEmail { get; set; }

		public string WorkPhone { get; set; }

		public string HomePhone { get; set; }

		public string HomeAddress { get; set; }

		public string WorkAddress { get; set; }

		public DateTime? TerminationDate { get; set; }

		public IList<CustomerOpportunityViewModel> Opportunities { get; set; }

		public IList<CustomerRiskViewModel> Risks { get; set; }
	}
}