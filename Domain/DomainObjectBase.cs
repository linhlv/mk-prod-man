using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class DomainObjectBase
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DomainObjectBase()
        {
            CreatedDate = DateTime.Today;
        }
    }
}