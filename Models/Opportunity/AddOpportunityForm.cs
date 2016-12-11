using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Kenrapid.CRM.Web.Models.Opportunity
{
    public class AddOpportunityForm : IMapTo<Kenrapid.CRM.Web.Domain.Opportunity>
	{
        [HiddenInput]
		public int CustomerId { get; set; }

        [Required]
		public string Title { get; set; }

        [DataType(DataType.MultilineText)]
		public string Description { get; set; }
	}
}