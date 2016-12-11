using System;

namespace Kenrapid.CRM.Web.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class Risk : DomainObjectBase
	{
        /// <summary>
        /// 
        /// </summary>
		public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
		public Customer Customer { get; set; }

	}
}