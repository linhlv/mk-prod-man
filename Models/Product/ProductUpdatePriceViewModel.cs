using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Kenrapid.CRM.Web.Infrastructure.Mapping;

namespace Kenrapid.CRM.Web.Models.Product
{
    public class ProductUpdatePriceViewModel : IMapTo<Kenrapid.CRM.Web.Domain.Product>
    {
        public int Id { get; set; }

        public decimal OldPrice { get; set; }

        [DataType("_Price"), DisplayName("Price")]
        public decimal ListPrice { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastPriceDate { get; set; }


        public override string ToString()
        {
            return string.Format("Price of {0} is changed from {1} to {2}", Id, OldPrice, ListPrice);
        }
    }
}