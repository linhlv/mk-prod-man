using System;

namespace Kenrapid.CRM.Web.Domain
{
    /// <summary>
    /// 
    /// </summary>
	public class Issue
	{
        /// <summary>
        /// 
        /// </summary>
		public int IssueID { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public ApplicationUser Creator { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public ApplicationUser AssignedTo { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string Subject { get; set; }
	
        /// <summary>
        /// 
        /// </summary>
		public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public IssueType IssueType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected Issue()
		{
			
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="assignedTo"></param>
        /// <param name="type"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
		public Issue(ApplicationUser creator, ApplicationUser assignedTo, IssueType type, string subject, string body)
		{
			Creator = creator;
			AssignedTo = assignedTo;
			Subject = subject;
			Body = body;
			CreatedAt = DateTime.Now;
			IssueType = type;
		}
	}
}