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
    public class Category : DomainObjectBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Category()
        {
            Products = new List<Product>();
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Product> Products { get; set; }

    }
}