using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Models.Quotation
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class QuotationModel
    {
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("ATTN")]
        public string Attn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Rate of Exchange"), DataType("_RateOfExchange")]
        public decimal RateOfExchange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DisplayName("Quotation Date"), DataType("_DateTime")]
        public DateTime QuotationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal DefaultRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<QuotationItem> QuotationItems
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasQuantity { get; set; }
    }
}