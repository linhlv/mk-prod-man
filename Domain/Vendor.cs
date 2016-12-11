using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class Vendor : DomainObjectBase
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
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
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Product> Products { get; set; }
    }
}