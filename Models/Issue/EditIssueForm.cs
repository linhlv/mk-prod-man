using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure.Mapping;
using Kenrapid.CRM.Web.Filters;
using System.ComponentModel;

namespace Kenrapid.CRM.Web.Models.Issue
{
    public class EditIssueForm : IMapTo<Domain.Issue>
    {
		[HiddenInput]
        public int IssueID { get; set; }

        [ReadOnly(true)]
        public string CreatorUserName { get; set; }
		public string Subject { get; set; }		
		public IssueType IssueType { get; set; }
        [Display(Name = "Assigned To")]
        public string AssignedToUserName { get; set; }
        public string Body { get; set; }        
	}
}