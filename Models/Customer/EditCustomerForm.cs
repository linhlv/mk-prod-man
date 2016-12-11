using AutoMapper;
using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System.Web.Mvc;

namespace Kenrapid.CRM.Web.Models
{
    public class EditCustomerForm : IMapTo<Kenrapid.CRM.Web.Domain.Customer>
	{
        [HiddenInput]
		public int Id { get; set; }

		public string Name { get; set; }

		public string WorkEmail { get; set; }

		public string HomeEmail { get; set; }

		public string WorkPhone { get; set; }

		public string HomePhone { get; set; }

		public string WorkAddress { get; set; }

		public string HomeAddress { get; set; }
	}
}