using AutoMapper;
using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Kenrapid.CRM.Web.Models
{
    public class AddCustomerForm : IMapTo<Kenrapid.CRM.Web.Domain.Customer>
	{
        [Required,Display(Name="Full Name",
            Prompt="Full Name (ex: John Doe)")]
		public string Name { get; set; }

        [Required, DataType(DataType.EmailAddress)]
		public string WorkEmail { get; set; }
        [Required, DataType(DataType.EmailAddress)]
		public string HomeEmail { get; set; }
        [DataType(DataType.PhoneNumber)]
		public string WorkPhone { get; set; }
        [DataType(DataType.PhoneNumber)]
		public string HomePhone { get; set; }
        [Required, DataType(DataType.MultilineText)]
		public string WorkAddress { get; set; }
        [DataType(DataType.MultilineText)]
		public string HomeAddress { get; set; }
	}
}