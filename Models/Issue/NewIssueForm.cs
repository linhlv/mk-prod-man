using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Filters;

namespace Kenrapid.CRM.Web.Models.Issue
{
    //public class NewIssueForm : IHaveIssueTypeSelectList, IHaveUserSelectList
    public class NewIssueForm 
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public IssueType IssueType { get; set; }        

        [Required, Display(Name="Assigned To")]
        public string AssignedToUserID { get; set; }
        [Required]        
        public string Body { get; set; }        
    }
}