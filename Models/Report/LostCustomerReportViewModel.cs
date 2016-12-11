using System;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.Report
{
	public class LostCustomerReportViewModel : IMapFrom<Domain.Customer>
	{
		public string Name { get; set; }

		public string WorkEmail { get; set; }

		public DateTime? TerminationDate { get; set; }
	}
}