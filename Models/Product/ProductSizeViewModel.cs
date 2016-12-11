using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.Product
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductSizeViewModel : IMapFrom<Kenrapid.CRM.Web.Domain.ProductSize>
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
    }
}