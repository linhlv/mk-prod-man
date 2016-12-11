using System;

namespace Kenrapid.CRM.Web.Domain
{
    /// <summary>
    /// 
    /// </summary>
	public class LogAction
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="performedBy"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="description"></param>
		public LogAction(ApplicationUser performedBy, string action, string controller, string description)
		{
			PerformedBy = performedBy;
			Action = action;
			Controller = controller;
			Description = description;
			PerformedAt = DateTime.Now;
		}

        /// <summary>
        /// 
        /// </summary>
		public int LogActionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime PerformedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string Controller { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string Action { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public ApplicationUser PerformedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string Description { get; set; }
	}
}