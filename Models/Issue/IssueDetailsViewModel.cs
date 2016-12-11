using System;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System.ComponentModel;
using Kenrapid.CRM.Web.Infrastructure.ModelMetadata;

namespace Kenrapid.CRM.Web.Models.Issue
{
    public class IssueDetailsViewModel : IMapFrom<Domain.Issue>
	{
        [Render(ShowForEdit=false)]
		public int IssueID { get; set; }
        [Render(ShowForEdit = false)]
        public DateTime CreatedAt { get; set; }
        [ReadOnly(true)]
        public string CreatorUserName { get; set; }
		public string Subject { get; set; }      
		public string AssignedToUserName { get; set; }
   
		public IssueType IssueType { get; set; }
		public string Body { get; set; }
	}
}