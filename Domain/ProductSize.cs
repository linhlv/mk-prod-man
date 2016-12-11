using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductSize
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Product Product { get; set; }
    }
}