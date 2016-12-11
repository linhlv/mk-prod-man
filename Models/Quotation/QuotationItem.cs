using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kenrapid.CRM.Web.Models.Quotation
{
    [Serializable]
    public class QuotationItem
    {
        public Guid ItemId { get; set; }
        public int ProductId { get; set; }
        public string CodeNo { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal PriceFOB { get; set; }
        public decimal PackingPCSSE { get; set; }
        public decimal PackingCBM { get; set; }
        public decimal CartonMeasurementW { get; set; }
        public decimal CartonMeasurementD { get; set; }
        public decimal CartonMeasurementH { get; set; }
    }
}