using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System;

namespace Kenrapid.CRM.Web.Models.Report
{
	public class NewCustomerReportViewModel : IMapFrom<Domain.Customer>
	{
		public string Name { get; set; }

		public string WorkEmail { get; set; }

		public DateTime CreateDate { get; set; }
	}
}