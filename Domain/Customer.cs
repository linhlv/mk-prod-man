using System;
using System.Collections.Generic;

namespace Kenrapid.CRM.Web.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class Customer : DomainObjectBase
	{
        /// <summary>
        /// 
        /// </summary>
		public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string WorkEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string HomeEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string WorkPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string HomePhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string HomeAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string WorkAddress { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
		public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public IList<Opportunity> Opportunities { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public IList<Risk> Risks { get; set; }

	}
}